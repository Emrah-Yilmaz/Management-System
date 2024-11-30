using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus;
using CommonLibrary.Extensions;
using CommonLibrary.Features.Paginations;
using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;
using ManagementSystem.Domain.PasswordEncryptor;
using ManagementSystem.Domain.Persistence.Comment;
using ManagementSystem.Domain.Persistence.Department;
using ManagementSystem.Domain.Persistence.Location;
using ManagementSystem.Domain.Persistence.NewFolder;
using ManagementSystem.Domain.Persistence.User;
using ManagementSystem.Domain.Persistence.WorkTask;
using ManagementSystem.Domain.Services.Abstract.User;
using ManagementSystem.Domain.TokenHandler;
using ManagementSystem.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Packages.Exceptions.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static ManagementSystem.Domain.Utilities.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ManagementSystem.Domain.Services.Concrete.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository,
            IConfiguration configuration,
            IMapper mapper,
            IDepartmentRepository departmentRepository,
            IProjectRepository projectRepository,
            IAddressRepository addressRepository,
            IWorkTaskRepository workTaskRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
            _addressRepository = addressRepository;
            _workTaskRepository = workTaskRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> AddUserToDepartment(AddUserToDepartmentArgs args, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(args.UserId);

            if (user is null)
                return default;
            
            var department = await _departmentRepository.GetByIdAsync(args.DepartmentId);
            if (department is null)
                return default;

            user.DepartmentId = department.Id;

            var result = await _userRepository.UpdateAsync(user, cancellationToken);
            if (result == 0)
            {
                throw new Exception();
            }

            return true;
        }

        public async Task<bool> AssignUserToProjectAsync(AssignUserToProjectArgs args, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FirstOrDefaultAsync(
                predicate: u => u.Id == args.UserId,
                isDeleted: false,
                noTracking: false,
                cancellationToken: default,
                includes: p => p.Projects);
                

            if (user is null)
                return false;

            var project = await _projectRepository.FirstOrDefaultAsync(
                predicate: p => p.Id == args.ProjectId,
                isDeleted: false,
                noTracking: false,
                cancellationToken: default,
                includes: u => u.Users);

            if (project is null)
                return false;

            user.Projects.Add(project);
            project.Users.Add(user);
            var result = await _userRepository.SaveChangeAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> ChangeStatusAsync(ChangeStatusArgs args, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(
                id: args.Id,
                noTracking: false,
                cancellationToken: default);

            if (user is null)
                throw new Exception("User not found");

            user.Status = args.Status.ToString();

            var result = await _userRepository.SaveChangeAsync(cancellationToken);

            return result > 0;
        }

        public async Task<int> CreateAsync(CreateUserArgs args, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Domain.Entities.User>(args);
            var hashedPass = Encrypt.Encript(args.PasswordHash);
            entity.PasswordHash = hashedPass;
            entity.Status = StatusType.Pending.ToString();
            var result = await _userRepository.AddAsync(entity, cancellationToken);

            //Todo
            //RabbitMQ implementasyonu sonrasında Doğrulama maili gönderilecek

            return result;
        }

        public async Task<bool> CreateUserAddressAsync(CreateAddressArgs args, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(args.UserId);
            if (user is null)
                return false;

            var userAddress = new Address()
            {
                CityId = args.CityId,
                DistrictId = args.DistrictId,
                QuerterId = args.QuarterId,
                UserId = args.UserId,
                Description = args.Description,
                Status = StatusType.Published.ToString()
            };
            user.Addresses ??= new List<Address>();
            user.Addresses.Add(userAddress);
            await _userRepository.UpdateAsync(user, cancellationToken);
            return true;
        }

        public async Task<bool> CreateUsersWithBogus(CancellationToken cancellationToken = default)
        {

            var faker = new Faker<Domain.Entities.User>("tr")
                        .RuleFor(u => u.Name, f => f.Name.FirstName())
                        .RuleFor(u => u.LastName, f => f.Name.LastName())
                        .RuleFor(u => u.UserName, (f, u) => $"{u.Name.ToLower()}.{u.LastName.ToLower()}")
                        .RuleFor(u => u.Email, f => f.Internet.Email())
                        .RuleFor(u => u.Status, StatusType.Published.ToString())
                        .RuleFor(u => u.PasswordHash, _ => "123456");

            var users = faker.Generate(100);
            var saved = await _userRepository.AddRangeAsync(users);
            if (saved == 0)
                return false;

            var departmentFaker = new Faker<Domain.Entities.Department>("tr")
                            .RuleFor(d => d.Name, f => f.Commerce.Department()) // Rastgele bir departman adı
                            .RuleFor(d => d.Users, f =>
                            {
                                // Kullanıcıları rastgele seçmek için int kullanıcı id'sini kullan
                                var userCount = users.Count;
                                var random = new Random();
                                return Enumerable.Range(0, 5) // 5 kullanıcı seç
                                                 .Select(_ => users[random.Next(userCount)]) // Rastgele kullanıcı seç
                                                 .Distinct() // Tekrar eden kullanıcıları kaldır
                                                 .ToList();
                            })
                            .RuleFor(d => d.Projects, f => new List<Project>()); // Projeler boş bir liste

            var departments = departmentFaker.Generate(10);
            var savedDepartments = await _departmentRepository.AddRangeAsync(departments);


            var projectFaker = new Faker<Project>("tr")
                            .RuleFor(p => p.Name, f => f.Commerce.ProductName()) // Rastgele bir proje adı
                            .RuleFor(p => p.Departments, f =>
                            {
                                var random = new Random();
                                return Enumerable.Range(0, 2)
                                                 .Select(_ => departments[random.Next(departments.Count)]) // Rastgele departman seç
                                                 .Distinct() // Tekrar eden departmanları kaldır
                                                 .ToList();
                            })
                            .RuleFor(p => p.Users, (f, p) =>
                            {
                                var random = new Random();
                                // Projeye ait departmanlardan kullanıcıları seçiyoruz
                                var selectedUsers = p.Departments
                                    .SelectMany(department => users.Where(u => u.DepartmentId == department.Id))
                                    .OrderBy(_ => random.Next()) // Rastgele sırala
                                    .Take(10) // 10 kullanıcı seç
                                    .Distinct() // Tekrar eden kullanıcıları kaldır
                                    .ToList();

                                return selectedUsers;
                            })
                            .RuleFor(p => p.WorkTasks, f => new List<Domain.Entities.WorkTask>()); // Çalışma görevleri boş bir liste

            // 10 adet proje oluşturma
            var projects = projectFaker.Generate(50);

            var savedProjects = await _projectRepository.AddRangeAsync(projects);

            return true;
        }

        public async Task<UserDto> GetUser(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetThenInclude(
                       predicate: u => u.Id == userId,
                        noTracking: true,
                        includes: new Func<IQueryable<Entities.User>, IQueryable<Entities.User>>[]
                        {
                            q => q.Include(d => d.Department).ThenInclude(p => p.Projects),
                            q => q.Include(u => u.Addresses), // User'dan Addresses'e geçiş
                            q => q.Include(u => u.Addresses).ThenInclude(a => a.City),// Address'ten City'e geçiş
                            q => q.Include(u => u.Addresses).ThenInclude(a => a.District), // Address'ten City'e geçiş
                            q => q.Include(u => u.Addresses).ThenInclude(a => a.Quarter) // Address'ten City'e geçiş
                        }).FirstOrDefaultAsync();
            if (user is null)
                return null;

            var adrresses = _addressRepository.Get(p => p.UserId == userId, false, c => c.City, d => d.District, q => q.Quarter);
            var mappedResult =  _mapper.Map<UserDto>(user);
            return mappedResult;
        }

        public async Task<PagedViewModel<UserDto>> GetUsers(GetUserArgs args, CancellationToken cancellationToken = default)
        {
            Func<IQueryable<Entities.User>, IQueryable<Entities.User>>[] includes = Array.Empty<Func<IQueryable<Entities.User>, IQueryable<Entities.User>>>();

            switch (args.UserRequestType)
            {
                case UserRequestType.Basic:
                    var basicUsers = _userRepository.GetThenInclude(
                        predicate: null,
                        noTracking: false,
                        includes: null);
                    return await Map(basicUsers, args);

                case UserRequestType.Address:
                    includes = new Func<IQueryable<Entities.User>, IQueryable<Entities.User>>[]
                    {
                        q => q.Include(u => u.Addresses), // User'dan Addresses'e geçiş
                        q => q.Include(u => u.Addresses).ThenInclude(a => a.City),// Address'ten City'e geçiş
                        q => q.Include(u => u.Addresses).ThenInclude(a => a.District), // Address'ten City'e geçiş
                        q => q.Include(u => u.Addresses).ThenInclude(a => a.Quarter) // Address'ten City'e geçiş
                    };
                    break;

                default:
                    includes = new Func<IQueryable<Entities.User>, IQueryable<Entities.User>>[]
                    {
                        q => q.Include(u => u.Department).ThenInclude(d => d.Projects),
                        q => q.Include(u => u.Addresses), // User'dan Addresses'e geçiş
                        q => q.Include(u => u.Addresses).ThenInclude(a => a.City),// Address'ten City'e geçiş
                        q => q.Include(u => u.Addresses).ThenInclude(a => a.District), // Address'ten City'e geçiş
                        q => q.Include(u => u.Addresses).ThenInclude(a => a.Quarter) // Address'ten City'e geçiş
                    };
                    break;
            }

            var users = _userRepository.GetThenInclude(
                predicate: null,
                noTracking: true,
                includes: includes);

            return await Map(users, args);
        }

        private async Task<PagedViewModel<UserDto>> Map(IQueryable<Domain.Entities.User> users, GetUserArgs args)
        {
            if (users is null)
                return null;

            var mappedResult = await users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .GetPaged(args.Page, args.PageSize);

            return mappedResult;
        }
        public async Task<LoginDto> LoginAsync(LoginArgs args, CancellationToken cancellationToken = default)
        {
            var dbUser = await _userRepository.GetThenInclude(u => u.UserName == args.UserName, false, q => q.Include(ur => ur.Roles)).FirstOrDefaultAsync();
            if (dbUser is null)
                return null;

            var hashedPass = Encrypt.Encript(args.Password);
            var isExistPass = dbUser.PasswordHash == hashedPass;
            if (!isExistPass)
            {
                throw new BusinessException("Kullanıcı adı ya da parola hatalı");
            }

            var model = new LoginDto
            {
                Id = dbUser.Id,
                FirstName = dbUser.Name,
                LastName = dbUser.LastName,
                UserName = dbUser.UserName,
            };

            var userRoles = dbUser?.Roles?.Select(p => p.Name).ToList();
            var claims = new List<Claim>
            {
                new Claim(Shared.JwtClaims.UserId, dbUser.Id.ToString()),
                new Claim(Shared.JwtClaims.Email, dbUser.Email),
                new Claim(Shared.JwtClaims.FirstName, dbUser.Name),
                new Claim(Shared.JwtClaims.LastName, dbUser.LastName),
                new Claim(Shared.JwtClaims.UserName, dbUser.UserName),
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            model.Token = GenerateToken(claims);

            return model;
        }

        public async Task<int> UpdateUserAddressAsync(UpdateAddressArgs args, CancellationToken cancellationToken = default)
        {
            var address = await _addressRepository.GetByIdAsync(args.Id);
            if (address is null)
                return default;
            
            var entity = args.Modify(address);
            var result = await _addressRepository.UpdateAsync(entity, cancellationToken);
            if (result == 0)
                return default;

            return result;
        }

        private string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiry,
                signingCredentials: creds,
                notBefore: DateTime.Now
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> CreateCommentToRandomTasks(int userId, CancellationToken cancellationToken = default)
        {
            var tasks = await _workTaskRepository.GetAllAsync(
                noTracking: false,
                cancellationToken);
            if (tasks is null || tasks.Count == 0)
                return false;

            var commentFaker = new Faker<Domain.Entities.Comment>("tr")
                .RuleFor(c => c.Content, f => f.Lorem.Sentence())
                .RuleFor(c => c.UserId, userId)
                .RuleFor(c => c.TaskId, t =>
                {
                    var random = new Random();
                    return tasks[random.Next(tasks.Count)].Id; // tasks listesinden rastgele bir görevin id'sini seçer
                });

            var user = await _userRepository.FirstOrDefaultAsync(
                predicate: u => u.Id == userId,
                isDeleted: false,
                noTracking: false,
                cancellationToken: default,
                c => c.Comments);

            var comments = commentFaker.Generate(tasks.Count);
            user.Comments ??= new List<Domain.Entities.Comment>();

            if (comments is not null && comments.Count > 0)
            {
                foreach (var comment in comments)
                {
                    user.Comments.Add(comment);
                }  
            }

            var result = await _userRepository.SaveChangeAsync();
            return result > 0;
        }

        public async Task<UserDto?> GetUserRoles(GetByIdArgs args, CancellationToken cancellationToken = default)
        {

            var user = await _userRepository.Get(
                     predicate: u => u.Id == args.Id,
                     includes: null)
            .Select(u => new UserDto 
            {
                Name = u.Name,
                LastName = u.LastName,
                Email = u.Email,
                UserName = u.UserName,
                Roles = u.Roles.Select(r => new RoleDto
                    {
                        Id = r.Id,
                        Name = r.Name
                    }).ToList()
            }).FirstOrDefaultAsync();

            if (user is null)
                return null;

            return user;
        }

        public async Task<bool> AssignRoleAsync(AssignRoleArgs args, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.Get(
                predicate: u => u.Id == args.Id,
                noTracking: false,
                includes: r => r.Roles).FirstOrDefaultAsync();

            if (user is null)
                throw new BusinessException(ErrorMessage.UserNotFound);

            var role = await _roleRepository.Get(
                predicate: r => r.Name == args.Roles.ToString(),
                noTracking: true).FirstOrDefaultAsync();

            if (role is null)
                throw new BusinessException(ErrorMessage.RoleNotFound);

            if (user.Roles.Any(r => r.Id == role.Id))
                throw new BusinessException(ErrorMessage.UserAlreadyHasRole);

            user.Roles.Add(role);

            return await _userRepository.UpdateAsync(user, cancellationToken) > 0 ; 
        }
    }
}

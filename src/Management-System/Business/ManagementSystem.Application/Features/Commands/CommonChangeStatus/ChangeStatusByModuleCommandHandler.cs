using ManagementSystem.Domain.Persistence.Comment;
using ManagementSystem.Domain.Persistence.Department;
using ManagementSystem.Domain.Persistence.Location;
using ManagementSystem.Domain.Persistence.User;
using ManagementSystem.Domain.Persistence.WorkTask;
using MediatR;
using Packages.Exceptions.Types;
using static ManagementSystem.Domain.Utilities.Shared;

namespace ManagementSystem.Application.Features.Commands.CommonChangeStatus
{
    public class ChangeStatusByModuleCommandHandler : IRequestHandler<ChangeStatusByModuleCommand>
    {
        IDepartmentRepository _departmentRepository;
        IWorkTaskRepository _workTaskrepository;
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;
        IAddressRepository _addressRepository;

        public ChangeStatusByModuleCommandHandler(IDepartmentRepository departmentRepository,
            IWorkTaskRepository workTaskrepository,
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            IAddressRepository addressRepository)
        {
            _departmentRepository = departmentRepository;
            _workTaskrepository = workTaskrepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _addressRepository = addressRepository;
        }

        public async Task Handle(ChangeStatusByModuleCommand request, CancellationToken cancellationToken)
        {
            if (request.ModulesType == Domain.Models.Enums.ModulesType.Department)
            {
                var entity = await _departmentRepository.FirstOrDefaultAsync
                    (
                        predicate: d => d.Id == request.Id,
                        isDeleted: false,
                        noTracking: false,
                        cancellationToken: cancellationToken
                    );
                if (entity is null)
                    throw new BusinessException(string.Format(ErrorMessage.NotFoundError, Entities.Department));

                entity.Status = request.StatusType.ToString();
                await _departmentRepository.SaveChangeAsync(cancellationToken);
            }
            else if (request.ModulesType == Domain.Models.Enums.ModulesType.Project)
            {
                var entity = await _projectRepository.FirstOrDefaultAsync
                    (
                        predicate: d => d.Id == request.Id,
                        isDeleted: false,
                        noTracking: false,
                        cancellationToken: cancellationToken
                    );
                if (entity is null)
                    throw new BusinessException(string.Format(ErrorMessage.NotFoundError, Entities.Project));

                entity.Status = request.StatusType.ToString();
                await _projectRepository.SaveChangeAsync(cancellationToken);
            }
            else if (request.ModulesType == Domain.Models.Enums.ModulesType.User)
            {
                var entity = await _userRepository.FirstOrDefaultAsync
                    (
                        predicate: d => d.Id == request.Id,
                        isDeleted: false,
                        noTracking: false,
                        cancellationToken: cancellationToken
                    );
                if (entity is null)
                    throw new BusinessException(string.Format(ErrorMessage.NotFoundError, Entities.User));

                entity.Status = request.StatusType.ToString();
                await _userRepository.SaveChangeAsync(cancellationToken);
            }
            else if (request.ModulesType == Domain.Models.Enums.ModulesType.WorkTask)
            {
                var entity = await _workTaskrepository.FirstOrDefaultAsync
                    (
                        predicate: d => d.Id == request.Id,
                        isDeleted: false,
                        noTracking: false,
                        cancellationToken: cancellationToken
                    );
                if (entity is null)
                    throw new BusinessException(string.Format(ErrorMessage.NotFoundError, Entities.WorkTask));

                entity.Status = request.StatusType.ToString();
                await _workTaskrepository.SaveChangeAsync(cancellationToken);
            }
        }
    }
}

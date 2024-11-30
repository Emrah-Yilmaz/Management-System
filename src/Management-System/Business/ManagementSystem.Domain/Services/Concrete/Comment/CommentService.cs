using AutoMapper;
using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Models.Args.Comment;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;
using ManagementSystem.Domain.Persistence.Comment;
using ManagementSystem.Domain.Services.Abstract.Comment;
using ManagementSystem.Domain.TokenHandler;

namespace ManagementSystem.Domain.Services.Concrete.Comment
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _repository;
        private readonly IDomainPrincipal _domainPrincipal;

        public CommentService(IMapper mapper, ICommentRepository repository, IDomainPrincipal domainPrincipal)
        {
            _mapper = mapper;
            _repository = repository;
            _domainPrincipal = domainPrincipal;
        }

        private Domain.Entities.Comment GetEntity(int id)
        {
            var entity = _repository.SingleOrDefaultAsync(
                predicate: p => p.Id == id && p.Status != StatusType.Deleted.ToString(),
                isDeleted: false,
                noTracking: false);

            return entity.Result;
        }

        private void ProcessOwner(Domain.Entities.Comment? entity)
        {
            var isExist = entity.CreatedById == _domainPrincipal.GetClaims().Id;
            if (!isExist)
            {
                throw new Exception("Bu işlem için yetkiniz bulunmamaktadır.");
            }
        }

        public async Task<int> CreateAsync(CreateCommentArgs args, CancellationToken cancellationToken = default)
        {
            var newEntity = _mapper.Map<Domain.Entities.Comment>(args);
            newEntity.Status = StatusType.Pending.ToString();
            var result = await _repository.AddAsync(newEntity, cancellationToken);

            return result;
        }

        public async Task<bool> DeleteAsync(GetByIdArgs args, CancellationToken cancellationToken = default)
        {
            var entity = GetEntity(args.Id);
            if (entity is null)
            {
                return false;
            }

            ProcessOwner(entity);

            entity.Status = StatusType.Deleted.ToString();
            var result = await _repository.UpdateAsync(entity, cancellationToken);
            if (result == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<GetCommentDto> GetAsync(GetByIdArgs args, CancellationToken cancellationToken = default)
        {
            var entity = GetEntity(args.Id);
            if (entity is null)
            {
                return null;
            }

            var mappedResult = _mapper.Map<GetCommentDto>(entity);

            return mappedResult;
        }

        public async Task<int> UpdateAsync(UpdateCommentArgs args, CancellationToken cancellationToken = default)
        {
            var entity = GetEntity(args.Id);
            if (entity is null)
            {
                return 0;
            }

            ProcessOwner(entity);

            var mappedResult = _mapper.Map(args, entity);

            var result = await _repository.UpdateAsync(mappedResult, cancellationToken);

            return result;
        } 

        public async Task<bool> ChangeStatus(ChangeStatusCommentArgs args, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FirstOrDefaultAsync(
                predicate: c => c.Id == args.CommentId,
                isDeleted: false,
                noTracking: false,
                cancellationToken: default,
                includes: null
            );
            if (entity is null)
                return false;

            ProcessOwner(entity);

            entity.Status = args.StatusType.ToString();
            var result = await _repository.SaveChangeAsync(cancellationToken: default);
            return result > 0;
        }

    }
}

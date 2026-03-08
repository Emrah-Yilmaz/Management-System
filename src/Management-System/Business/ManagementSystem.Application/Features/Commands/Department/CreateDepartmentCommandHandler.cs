using CommonLibrary.Templates.Mail;
using ManagementSystem.Domain.Events.DepartmentEvents;
using ManagementSystem.Domain.Services.Abstract.Department;
using ManagementSystem.Domain.TokenHandler;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Department
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly IDepartmentService _service;
        private readonly IMediator _mediator;
        private readonly IDomainPrincipal _domainPrincipal;

        public CreateDepartmentCommandHandler(IDepartmentService service, IMediator mediator, IDomainPrincipal domainPrincipal)
        {
            _service = service;
            _mediator = mediator;
            _domainPrincipal = domainPrincipal;
        }

        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(request, cancellationToken);

            var @event = new DepartmentCreated()
            {
                Id = result,
                Title = request.Name,
                CreatedOn = DateTime.Now,
                CreatedBy = $"{_domainPrincipal.GetClaims().Name} {_domainPrincipal.GetClaims().LastName}",
                Subject = "Departman Oluşturuldu",
                To = MailTemplate.Admin,
                ModulesType = CommonLibrary.Models.Enums.ModulesType.Department
            };

            await _mediator.Publish(@event, cancellationToken);
            return result;
        }
    }
}
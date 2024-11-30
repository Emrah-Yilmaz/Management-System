using ManagementSystem.Domain.Models.Enums;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.CommonChangeStatus
{
    public class ChangeStatusByModuleCommand : IRequest
    {
        public int Id { get; set; }
        public ModulesType ModulesType { get; set; }
        public StatusType StatusType { get; set; }
    }
}

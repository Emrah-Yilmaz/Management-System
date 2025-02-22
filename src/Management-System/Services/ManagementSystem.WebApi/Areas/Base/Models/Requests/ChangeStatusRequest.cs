using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.WebApi.Areas.Base.Models.Requests
{
    public class ChangeStatusRequest
    {
        public ModulesType ModulesType { get; set; }
        public StatusType StatusType { get; set; }
    }
}

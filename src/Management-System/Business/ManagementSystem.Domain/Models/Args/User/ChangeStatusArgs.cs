using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.Domain.Models.Args.User
{
    public class ChangeStatusArgs
    {
        public int Id { get; set; }
        public StatusType Status { get; set; }
        public ModulesType ModulesType { get; set; }
    }
}

using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.Domain.Models.Args.User
{
    public class AssignRoleArgs : GetByIdArgs
    {
        public Roles Roles { get; set; }
    }
}

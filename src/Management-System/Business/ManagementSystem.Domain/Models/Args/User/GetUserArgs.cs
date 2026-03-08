using CommonLibrary.Models.Requests;
using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.Domain.Models.Args.User
{
    public class GetUserArgs : PagedRequest
    {
        public UserRequestType UserRequestType { get; set; }
        public int? DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}

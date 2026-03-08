using ManagementSystem.Domain.Models.Args.User;

namespace ManagementSystem.WebApi.Areas.Users.Models.Requests
{
    public class GetUsersRequest : GetUserArgs
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
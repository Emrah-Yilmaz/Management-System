namespace ManagementSystem.WebApi.Areas.Admin.User.Models.Requests
{
    public class SearchUsersRequest
    {
        public int? DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}

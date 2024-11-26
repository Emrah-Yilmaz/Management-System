using ManagementSystem.WebApi.Models.Response;

namespace ManagementSystem.WebApi.Areas.Users.Models.Responses
{
    public class DepartmentOfUserResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ProjectResponse>? Projects { get; set; }
    }
}

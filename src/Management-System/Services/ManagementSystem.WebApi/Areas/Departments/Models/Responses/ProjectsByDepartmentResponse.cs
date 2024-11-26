using ManagementSystem.WebApi.Models.Response;

namespace ManagementSystem.WebApi.Areas.Departments.Models.Responses
{
    public class ProjectsByDepartmentResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ProjectResponse>? Projects { get; set; }
    }
}

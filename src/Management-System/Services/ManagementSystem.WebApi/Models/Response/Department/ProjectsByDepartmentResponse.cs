namespace ManagementSystem.WebApi.Models.Response.Department
{
    public class ProjectsByDepartmentResponse 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ProjectResponse>? Projects{ get; set; }
    }
}

using ManagementSystem.WebApi.Areas.Users.Models.Responses;

namespace ManagementSystem.WebApi.Models.Response
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserInfoResponse>? Users { get; set; }
    }
}
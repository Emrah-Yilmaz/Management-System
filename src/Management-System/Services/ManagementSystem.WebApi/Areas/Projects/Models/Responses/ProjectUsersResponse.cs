using ManagementSystem.WebApi.Areas.Users.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Projects.Models.Responses;
public class ProjectUsersResponse{
    public int Id {get; set;}
    public string Name {get; set;}
    public List<UserInfoResponse>? Users {get; set;}
}
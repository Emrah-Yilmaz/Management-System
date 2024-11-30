namespace ManagementSystem.Domain.Models.Dto;
public class ProjectUsersDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<UserDto>? Users { get; set; }
}
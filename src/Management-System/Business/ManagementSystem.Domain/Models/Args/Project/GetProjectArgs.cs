using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.Domain.Models.Args.Project;
public class GetProjectArgs
{
    public int Id { get; set; }
    public ProjectRequestType Type { get; set; }
}
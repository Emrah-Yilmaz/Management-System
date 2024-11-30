
using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.WebApi.Areas.Projects.Models.Requests;
public class GetProjectRequest
{
    public ProjectRequestType Type {get; set;}
}
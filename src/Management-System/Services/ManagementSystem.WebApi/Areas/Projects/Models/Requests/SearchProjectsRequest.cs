
using CommonLibrary.Features.Paginations;

namespace ManagementSystem.WebApi.Areas.Projects.Models.Requests;
public class SearchProjectsRequest : BasePagedQuery
{
    public string? Name {get; set;}
}
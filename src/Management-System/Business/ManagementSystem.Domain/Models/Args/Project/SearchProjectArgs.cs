namespace ManagementSystem.Domain.Models.Args.Project;
public class SearchProjectArgs
{
    public string? Name { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
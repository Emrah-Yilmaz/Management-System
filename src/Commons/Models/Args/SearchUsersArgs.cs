namespace CommonLibrary.Models.Args
{
    public class SearchUsersArgs
    {
        public int? DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}

using ManagementSystem.Domain.Models.Args.User;

namespace ManagementSystem.WebApi.Areas.Users.Models.Requests
{
    public class GetUsersRequest : GetUserArgs
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
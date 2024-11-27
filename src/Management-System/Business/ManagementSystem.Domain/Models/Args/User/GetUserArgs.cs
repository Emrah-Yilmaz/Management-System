using CommonLibrary.Models.Requests;
using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.Domain.Models.Args.User
{
    public class GetUserArgs : PagedRequest
    {
        public UserRequestType UserRequestType { get; set; }
    }
}

using ManagementSystem.WebApi.Areas.Locations.Models.Responses;
using ManagementSystem.WebApi.Areas.Users.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Admin.User.Models.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<AddressResponse>? Addresses { get; set; }
        public DepartmentOfUserResponse? Department { get; set; }
        public List<RoleResponse>? Roles { get; set; }
    }
}

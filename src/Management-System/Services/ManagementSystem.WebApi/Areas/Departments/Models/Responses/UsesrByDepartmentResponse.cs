using ManagementSystem.WebApi.Areas.Users.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Departments.Models.Responses
{
    public class UsesrByDepartmentResponse
    {
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int WorkersCount { get; set; }
        public IList<UserInfoResponse> Users { get; set; }
    }
}

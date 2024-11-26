using ManagementSystem.WebApi.Areas.Base.Models;
using ManagementSystem.WebApi.Areas.Comments.Models.Responses;

namespace ManagementSystem.WebApi.Areas.WorkTasks.Models.Responses
{
    public class WorkTasksResponse : BaseResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string Status { get; set; }
        public string AssignedUser { get; set; }
        public ICollection<GetCommentResponse> Comments { get; set; }
    }
}

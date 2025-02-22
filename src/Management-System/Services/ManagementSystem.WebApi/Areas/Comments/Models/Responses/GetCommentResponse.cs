﻿using ManagementSystem.WebApi.Areas.Base.Models.Responses;

namespace ManagementSystem.WebApi.Areas.Comments.Models.Responses
{
    public class GetCommentResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

﻿namespace ManagementSystem.WebApi.Areas.Base.Models.Responses
{
    public class BaseResponse
    {
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? CreatedById { get; set; }
        public string? CreatedBy { get; set; }
        public int? ModifiedById { get; set; }
        public string? ModifiedBy { get; set; }
    }
}

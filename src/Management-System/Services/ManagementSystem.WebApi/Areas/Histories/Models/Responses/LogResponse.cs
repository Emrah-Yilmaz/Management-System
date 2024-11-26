namespace ManagementSystem.WebApi.Areas.Histories.Models.Responses
{
    public class LogResponse
    {
        public int RecordId { get; set; }
        public string? OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime ChangedDate { get; set; }
        public ChangedByInfo ChangedBy { get; set; }
    }
}

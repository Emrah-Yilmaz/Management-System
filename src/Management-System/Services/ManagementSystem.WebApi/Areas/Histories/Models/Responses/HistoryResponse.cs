namespace ManagementSystem.WebApi.Areas.Histories.Models.Responses
{
    public class HistoryResponse
    {
        public string Entity { get; set; }
        public List<LogResponse> Logs { get; set; }
    }
}

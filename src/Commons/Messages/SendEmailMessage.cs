using CommonLibrary.Models.Enums;

namespace CommonLibrary.Messages
{
    public class SendEmailMessage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn  { get; set; }
        public string CreatedBy { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public ModulesType ModulesType { get; set; }
    }
}

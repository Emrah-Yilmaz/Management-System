using CommonLibrary.Messages;
using CommonLibrary.Templates.Mail;
using ManagementSystem.Notification.Services;
using MassTransit;

namespace ManagementSystem.Notification.Consumers.EmailConsumers
{
    public class SendEmailConsumer : IConsumer<SendEmailMessage>
    {
        public IEmailService _emailService;

        public SendEmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<SendEmailMessage> context)
        {
            var template = string.Format(MailTemplate.CreatedDepartment, context.Message.CreatedOn, context.Message.CreatedBy, context.Message.Id, context.Message.Title);
            await _emailService.SendEmailAsync(context.Message.To, context.Message.Subject, template);
        }
    }
}

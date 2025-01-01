using CommonLibrary.Messages;
using CommonLibrary.Templates.Mail;
using ManagementSystem.Notification.Services;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ManagementSystem.Notification.Consumers.EmailConsumers
{
    public class SendEmailConsumer : IConsumer<SendEmailMessage>
    {
        public IEmailService _emailService;

        public SendEmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task Consume(ConsumeContext<SendEmailMessage> context)
        {
            var template = string.Format(MailTemplate.CreatedDepartment, context.Message.CreatedOn, context.Message.CreatedBy, context.Message.Id, context.Message.Title);
            _emailService.SendEmailAsync(context.Message.To, context.Message.Subject, template);
            return Task.CompletedTask;
        }
    }
}

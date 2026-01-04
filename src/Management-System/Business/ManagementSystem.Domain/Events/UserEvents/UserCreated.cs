using CommonLibrary.Messages;
using MediatR;

namespace ManagementSystem.Domain.Events.UserEvents
{
    public class UserCreated : SendEmailMessage, INotification
    {
    }
}
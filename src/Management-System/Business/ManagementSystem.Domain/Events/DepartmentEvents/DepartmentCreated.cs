using CommonLibrary.Messages;
using MediatR;

namespace ManagementSystem.Domain.Events.DepartmentEvents
{
    public class DepartmentCreated : SendEmailMessage, INotification
    {
    }
}

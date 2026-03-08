using AutoMapper;
using CommonLibrary.Messages;
using MassTransit;
using MediatR;

namespace ManagementSystem.Domain.Events.UserEvents
{
    internal class UserCreatedEventHandler : INotificationHandler<UserCreated>
    {
        private readonly IBusControl _busControl;
        private readonly IMapper _mapper;

        public UserCreatedEventHandler(IMapper mapper, IBusControl busControl)
        {
            _mapper = mapper;
            _busControl = busControl;
        }

        public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
        {
            var mappedMessage = _mapper.Map<SendEmailMessage>(notification);
            await _busControl.Publish(mappedMessage, cancellationToken);
        }
    }
}
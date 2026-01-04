using AutoMapper;
using CommonLibrary.Messages;
using MassTransit;
using MediatR;

namespace ManagementSystem.Domain.Events.DepartmentEvents
{
    public class DepartmentCreatedEventHandler : INotificationHandler<DepartmentCreated>
    {
        private readonly IBusControl _busControl;
        private readonly IMapper _mapper;

        public DepartmentCreatedEventHandler(IBusControl busControl, IMapper mapper)
        {
            _busControl = busControl;
            _mapper = mapper;
        }

        public async Task Handle(DepartmentCreated notification, CancellationToken cancellationToken)
        {
            var mappedMessage = _mapper.Map<SendEmailMessage>(notification);
            await _busControl.Publish(mappedMessage, cancellationToken);
        }
    }
}

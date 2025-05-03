using MicrosLabel.Domain.Events;

namespace MicrosLabel.Domain.Events;

public interface IEventHandler<in TDomainEvent> where TDomainEvent : class, IEvent
{
    Task HandleAsync(TDomainEvent @event);

    bool CanHandle(TDomainEvent @event);
}

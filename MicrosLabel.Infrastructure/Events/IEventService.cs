namespace MicrosLabel.Domain.Events;

public interface IEventService
{
    void AddEvent(IEvent @event);

    Task RaiseEventsAsync();
}

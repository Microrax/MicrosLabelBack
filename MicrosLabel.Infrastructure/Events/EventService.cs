using MicrosLabel.Domain.Events;

namespace MicrosLabel.Infrastructure.Events;

public sealed class EventService : IEventService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICollection<IEvent> EventsPool;

    public EventService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        EventsPool = new List<IEvent>();
    }

    public void AddEvent(IEvent @event)
    {
        EventsPool.Add(@event);
    }

    public async Task RaiseEventsAsync()
    {
        var events = EventsPool.ToArray();
        EventsPool.Clear();

        foreach (var @event in events)
        {
            var collectionType = typeof(IEnumerable<>)
                .MakeGenericType(typeof(IEventHandler<>)
                .MakeGenericType(@event.GetType()));

            var handlers = _serviceProvider.GetService(collectionType) as IEnumerable<dynamic>;

            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    Type handlerType = handler.GetType();

                    var canHandleMethod = handlerType
                        .GetMethods()
                        .SingleOrDefault(m => m.Name == "CanHandle" && m.GetParameters().Any(p => p.ParameterType.FullName == @event.GetType().FullName));

                    var canHandleEvent = canHandleMethod != null && canHandleMethod.Invoke(handler, new object[] { @event });
                    if (canHandleEvent)
                    {
                        var handleMethod = handlerType
                            .GetMethods()
                            .Single(m => m.Name == "HandleAsync" && m.GetParameters().Any(p => p.ParameterType.FullName == @event.GetType().FullName));

                        await handleMethod.Invoke(handler, new object[] { @event });
                    }
                }
            }
        }

        if (EventsPool.Any())
        {
            await RaiseEventsAsync();
        }
    }
}

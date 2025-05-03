using MicrosLabel.Domain.Events;

namespace MicrosLabel.Domain.Aggregates;

public abstract class Aggregate
{
    protected Aggregate()
    {
        Id = Guid.NewGuid().ToString();
        DomainEvents = new List<IEvent>();
    }

    public virtual string Id { get; protected set; }

    public ICollection<IEvent> DomainEvents { get; private set; }

    public void AddDomainEvent(IEvent @event)
    {
        DomainEvents.Add(@event);
    }
}

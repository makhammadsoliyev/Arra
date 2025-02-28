namespace Arra.SharedKernel;

public abstract class Entity(Guid id) : IEntity
{
    public Guid Id { get; protected set; } = id;


    private readonly List<IDomainEvent> domainEvents;

    public List<IDomainEvent> DomainEvents => [.. domainEvents];


    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }
}

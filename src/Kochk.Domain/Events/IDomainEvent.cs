namespace Kochk.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}


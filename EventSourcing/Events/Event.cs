namespace EventSourcing.Events;


public abstract record Event
{
    public abstract Guid StreamId { get; }

    public required DateTime CreatedAt { get; set; }
}

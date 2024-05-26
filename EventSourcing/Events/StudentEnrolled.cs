namespace EventSourcing.Events;

public record StudentEnrolled : Event
{
    public override Guid StreamId => StudentId;
    public required Guid StudentId { get; set; }

    public required string CourseName { get; set; }
}

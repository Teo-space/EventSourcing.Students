namespace EventSourcing.Events;


public record StudentUnEnrolled : Event
{
    public override Guid StreamId => StudentId;
    public required Guid StudentId { get; set; }

    public required string CourseName { get; set; }
}
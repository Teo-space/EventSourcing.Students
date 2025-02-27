﻿namespace EventSourcing.Events;

public record StudentCreated : Event
{
    public override Guid StreamId => StudentId;

    public required Guid StudentId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required DateTime DateOfBirth { get; set; }
}

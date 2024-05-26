namespace EventSourcing.Infrastructure;

using EventSourcing.Domain;
using EventSourcing.Events;
using System.Collections.Generic;

public class StudentDatabase
{
    private readonly Dictionary<Guid, SortedList<DateTime, Event>>
        _studentEvents = new Dictionary<Guid, SortedList<DateTime, Event>>();

    private readonly Dictionary<Guid, Student> _studentsView = new Dictionary<Guid, Student>();

    public void Append(Event @event)
    {
        if(!_studentEvents.TryGetValue(@event.StreamId, out var stream))
        {
            stream = new SortedList<DateTime, Event>();
            _studentEvents[@event.StreamId] = stream;
        }

        stream.Add(@event.CreatedAt, @event);

        UpdateView(@event, stream);
    }

    private void UpdateView(Event @event, SortedList<DateTime, Event> stream)
    {
        if (!_studentsView.TryGetValue(@event.StreamId, out var student))
        {
            student = new Student();
            student.Apply(stream);
            _studentsView[@event.StreamId] = student;
        }
        else
        {
            student.Apply(@event);
        }
    }

    public Student? Get(Guid StudentId)
    {
        if(_studentEvents.TryGetValue(StudentId, out var stream))
        {
            var student = new Student();

            student.Apply(stream);

            return student;
        }

        return default;
    }

    public Student? GetView(Guid StudentId) => _studentsView[StudentId];

}

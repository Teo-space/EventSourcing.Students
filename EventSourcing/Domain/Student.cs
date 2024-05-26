using EventSourcing.Events;

namespace EventSourcing.Domain;

public record Student
{
    public Guid StudentId { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    public List<string> EnrolledCourses { get; private set; } 
        = new List<string>();


    private void Apply(StudentCreated studentCreated)
    {
        StudentId = studentCreated.StudentId;
        FullName = studentCreated.FullName;
        Email = studentCreated.Email;
        DateOfBirth = studentCreated.DateOfBirth;
    }

    private void Apply(StudentUpdated studentUpdated)
    {
        FullName = studentUpdated.FullName;
        Email = studentUpdated.Email;
        DateOfBirth = studentUpdated.DateOfBirth;
    }

    private void Apply(StudentEnrolled studentEnrolled)
    {
        if (!EnrolledCourses.Contains(studentEnrolled.CourseName))
        {
            EnrolledCourses.Add(studentEnrolled.CourseName);
        }
    }

    private void Apply(StudentUnEnrolled studentUnEnrolled)
    {
        if (EnrolledCourses.Contains(studentUnEnrolled.CourseName))
        {
            EnrolledCourses.Remove(studentUnEnrolled.CourseName);
        }
    }

    public void Apply(Event @event)
    {
        if (@event is StudentCreated studentCreated) 
        {
            Apply(studentCreated);
        }
        else if (@event is StudentUpdated studentUpdated) 
        {
            Apply(studentUpdated);
        }
        else if (@event is StudentEnrolled studentEnrolled) 
        {
            Apply(studentEnrolled);
        }
        else if (@event is StudentUnEnrolled studentUnEnrolled) 
        {
            Apply(studentUnEnrolled);
        }
        else
        {
            throw new Exception("Unsupported Event");
        }
    }

    public void Apply(IReadOnlyCollection<KeyValuePair<DateTime, Event>> @events)
    {
        foreach (var @event in @events)
        {
            Apply(@event.Value);
        }
    }

}

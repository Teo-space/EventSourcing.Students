using EventSourcing.Events;
using EventSourcing.Infrastructure;


var studentDatabase = new StudentDatabase();

Guid studentId = Guid.NewGuid();

var studentCreated = new StudentCreated
{
    StudentId = studentId,
    CreatedAt = DateTime.UtcNow,
    FullName = "Joshua Lorenz",
    Email = "joshua.lorenz@gmail.com",
    DateOfBirth = new DateTime(1985, 1, 1)
};

studentDatabase.Append(studentCreated);

var studentEnrolled1 = new StudentEnrolled 
{
    StudentId = studentId,
    CreatedAt = DateTime.UtcNow,
    CourseName = "Math" 
};

studentDatabase.Append(studentEnrolled1);

var studentEnrolled2 = new StudentEnrolled 
{
    StudentId = studentId,
    CreatedAt = DateTime.UtcNow,
    CourseName = "English Language" 
};

studentDatabase.Append(studentEnrolled2);

var studentUnEnrolled = new StudentUnEnrolled 
{
    StudentId = studentId,
    CreatedAt = DateTime.UtcNow,
    CourseName = "English Language" 
};

studentDatabase.Append(studentUnEnrolled);


var student = studentDatabase.Get(studentId);

Console.WriteLine("Student: ");
Console.WriteLine(student);
Console.WriteLine("EnrolledCourses: ");
student.EnrolledCourses.ForEach(e =>  Console.WriteLine(e));

var studentView = studentDatabase.GetView(studentId);

Console.WriteLine("StudentView: ");
Console.WriteLine(studentView);
Console.WriteLine("EnrolledCourses: ");
studentView.EnrolledCourses.ForEach(e => Console.WriteLine(e));


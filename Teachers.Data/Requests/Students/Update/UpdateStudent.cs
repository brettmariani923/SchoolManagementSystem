using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Students.Update
{
    public class UpdateStudent : IDataExecute
    {
        private readonly Students_Row _student;
        public UpdateStudent(Students_Row student)
        {
            _student = student ?? throw new ArgumentNullException(nameof(student));
            if (_student.StudentID <= 0)
                throw new ArgumentException("StudentID must be a positive existing ID.", nameof(student));
        }
        public string GetSql() =>
            @"UPDATE dbo.Students " +
            "SET StudentID = @StudentID, " +
            "TeacherID = @TeacherID, " +
            "CourseID  = @CourseID, " +
            "SchoolID  = @SchoolID " +
            "WHERE StudentID = @StudentID;";

        public object GetParameters() => new
        {
            _student.StudentID,
            _student.FirstName,
            _student.LastName,
            _student.Year,
            _student.SchoolID
        };
    }
}

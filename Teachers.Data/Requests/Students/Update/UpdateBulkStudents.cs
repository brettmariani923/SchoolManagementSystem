using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Students.Update
{
    public class UpdateBulkStudents : IDataExecute
    {
        private readonly IEnumerable<Students_Row> _students;

        public UpdateBulkStudents(IEnumerable<Students_Row> students)
        {
            _students = students;
        }

        public string GetSql() =>
        "UPDATE dbo.Students " +
        "SET StudentID = @StudentID, " +
        "TeacherID = @TeacherID, " +
        "CourseID  = @CourseID, " +
        "SchoolID  = @SchoolID " +
        "WHERE StudentID = @StudentID;";

        public object? GetParameters() =>
            _students.Select(s => new
            {
                s.StudentID,
                s.FirstName,
                s.LastName,
                s.Year,
                s.SchoolID
            });
    }
}

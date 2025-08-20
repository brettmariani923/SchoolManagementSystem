using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Students
{
    public class UpdateBulkStudents : IDataExecute
    {
        private readonly IEnumerable<Students_DTO> _students;

        public UpdateBulkStudents(IEnumerable<Students_DTO> students)
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

using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public class ReturnEnrollmentsByStudentID : IDataFetchList<Enrollments_DTO>
    {
        private readonly int _studentID;

        public ReturnEnrollmentsByStudentID(int studentID)
        {
            _studentID = studentID;
        }

        public string GetSql() => "SELECT * FROM dbo.Enrollments WHERE StudentID = @StudentID;";

        public object? GetParameters() => new { StudentID = _studentID };
    }
}

using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public class ReturnEnrollmentsByStudentID : IDataFetchList<Enrollments_Row>
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

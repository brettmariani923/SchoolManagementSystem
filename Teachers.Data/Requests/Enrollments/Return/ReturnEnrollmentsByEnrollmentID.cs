using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public class ReturnEnrollmentsByEnrollmentID : IDataFetchList<Enrollments_DTO>
    {
        private readonly int _enrollmentID;

        public ReturnEnrollmentsByEnrollmentID(int enrollmentID)
        {
            _enrollmentID = enrollmentID;
        }

        public string GetSql() => "SELECT * FROM dbo.Enrollments WHERE EnrollmentID = @EnrollmentID;";

        public object? GetParameters() => new { EnrollmentID = _enrollmentID };
    }
}

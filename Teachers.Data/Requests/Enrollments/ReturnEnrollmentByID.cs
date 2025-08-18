using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Enrollments
{
    public class ReturnEnrollmentByID : IDataFetch<Enrollments_DTO>
    {
        private readonly int _enrollmentID;

        public ReturnEnrollmentByID(int enrollmentID)
        {
            _enrollmentID = enrollmentID;
        }

        public string GetSql() =>
            @"SELECT EnrollmentID, TeacherID, StudentID, CourseID, SchoolID
              FROM dbo.Enrollments
              WHERE EnrollmentID = @EnrollmentID;";

        public object GetParameters() => new { EnrollmentID = _enrollmentID };
    }
}

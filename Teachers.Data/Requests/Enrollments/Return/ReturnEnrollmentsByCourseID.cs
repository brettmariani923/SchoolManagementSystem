using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public class ReturnEnrollmentsByCourseID : IDataFetchList<Enrollments_DTO>
    {
        private readonly int _courseID;

        public ReturnEnrollmentsByCourseID(int courseID)
        {
            _courseID = courseID;
        }

        public string GetSql() =>
            @"SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID
                FROM dbo.Enrollments
                WHERE CourseID = _courseID
                ORDER BY EnrollmentID;";

        public object? GetParameters() => new { CourseID = _courseID };
    }
}

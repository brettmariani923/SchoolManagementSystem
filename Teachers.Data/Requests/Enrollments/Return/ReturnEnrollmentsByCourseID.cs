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

        public string GetSql() => "SELECT * FROM dbo.Enrollments WHERE CourseID = @CourseID;";

        public object? GetParameters() => new { CourseID = _courseID };
    }
}

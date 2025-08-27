using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public sealed class ReturnEnrollmentsByCourseID : IDataFetchList<Enrollments_Row>
    {
        private readonly int _courseID;

        public ReturnEnrollmentsByCourseID(int courseID)
        {
            if (courseID <= 0)
                throw new ArgumentOutOfRangeException(nameof(courseID), "CourseID must be positive.");

            _courseID = courseID;
        }

        public string GetSql() =>
            @"SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID " +
              "FROM dbo.Enrollments " +
              "WHERE CourseID = @CourseID;";

        public object GetParameters() => new { CourseID = _courseID };
    }
}

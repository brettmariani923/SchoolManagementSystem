using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Courses.Return
{
    public class ReturnCourseByID : IDataFetch<Courses_Row>
    {
        private readonly int _courseID;

        public ReturnCourseByID(int courseID)
        {
            if (courseID <= 0)
                throw new ArgumentOutOfRangeException(nameof(courseID), "CourseID must be positive.");

            _courseID = courseID;
        }

        public string GetSql() =>
            @"SELECT CourseID, CourseName, Credits, SchoolID " +
              "FROM dbo.Courses " +
              "WHERE CourseID = @CourseID;";

        public object GetParameters() => new { CourseID = _courseID };
    }
}

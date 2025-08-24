using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Courses.Remove
{
    public class RemoveCourseByID : IDataExecute
    {
        private readonly int _courseID;

        public RemoveCourseByID(int courseID)
        {
            if (courseID <= 0)
                throw new ArgumentOutOfRangeException(nameof(courseID), "CourseID must be positive.");

            _courseID = courseID;
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Courses " +
             "WHERE CourseID = @CourseID;";

        public object GetParameters() => new { CourseID = _courseID };
    }
}

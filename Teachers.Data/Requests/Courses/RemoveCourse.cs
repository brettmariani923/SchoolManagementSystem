using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Courses
{
    public class RemoveCourse : IDataExecute
    {
        private readonly int _courseID;

        public RemoveCourse(int courseID)
        {
            _courseID = courseID;
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Courses
              WHERE CourseID = @CourseID;";

        public object GetParameters() => new { CourseID = _courseID };
    }
}

using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Courses
{
    public class RemoveBulkCourses : IDataExecute
    {
        private readonly IEnumerable<int> _courseIDs;

        public RemoveBulkCourses(IEnumerable<int> courseIDs)
        {
            _courseIDs = courseIDs ?? throw new ArgumentNullException(nameof(courseIDs));
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Courses
              WHERE CourseID = @CourseID;";

        public object? GetParameters() =>
            _courseIDs.Select(id => new { CourseID = id });
    }
}

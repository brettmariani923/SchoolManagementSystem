using Teachers.Data.Interfaces;

namespace Teachers.Data.Requests.Courses.Remove
{
    public class RemoveBulkCourses : IDataExecute
    {
        private readonly int[] _courseIDs;

        public RemoveBulkCourses(IEnumerable<int> courseIDs)
        {
            if (courseIDs is null) throw new ArgumentNullException(nameof(courseIDs));
            _courseIDs = courseIDs.Distinct().ToArray();
            if (_courseIDs.Length == 0)
                throw new ArgumentException("At least one CourseID is required.", nameof(courseIDs));
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Courses " +
             "WHERE CourseID IN @CourseIDs;";

        public object? GetParameters() => new { CourseIDs = _courseIDs };
    }
}

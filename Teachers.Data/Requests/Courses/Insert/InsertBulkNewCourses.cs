using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Courses.Insert
{
    public sealed class InsertBulkNewCourses : IDataExecute
    {
        private readonly IEnumerable<Courses_Row> _courses;

        public InsertBulkNewCourses(IEnumerable<Courses_Row> courses)
        {
            _courses = courses ?? throw new ArgumentNullException(nameof(courses));
            if (!_courses.Any())
                throw new ArgumentException("At least one course is required.", nameof(courses));
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Courses (CourseName, Credits, SchoolID)" +
             "VALUES (@CourseName, @Credits, @SchoolID);";

        public object GetParameters() =>
            _courses.Select(c => new
            {
                c.CourseName,
                c.Credits,
                c.SchoolID
            });
    }
}

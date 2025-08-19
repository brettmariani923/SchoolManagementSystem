using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Courses
{
    public class InsertBulkNewCourses : IDataExecute
    {
        private readonly IEnumerable<Courses_DTO> _courses;

        public InsertBulkNewCourses(IEnumerable<Courses_DTO> courses)
        {
            _courses = courses;
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Courses (CourseID, CourseName, Credits, SchoolID)" +
              "VALUES (@CourseID, @CourseName, @Credits, @SchoolID);";

        public object? GetParameters() =>
            _courses.Select(c => new
            {
                c.CourseID,
                c.CourseName,
                c.Credits,
                c.SchoolID
            });
    }
}

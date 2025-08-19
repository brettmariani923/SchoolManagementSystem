using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Courses.Update
{
    public class UpdateBulkCourses : IDataExecute
    {
        private readonly IEnumerable<Courses_DTO> _courses;

        public UpdateBulkCourses(IEnumerable<Courses_DTO> courses)
        {
            _courses = courses ?? throw new ArgumentNullException(nameof(courses));
            if (!_courses.Any())
                throw new ArgumentException("At least one course is required.", nameof(courses));
        }

        public string GetSql() =>
            @"UPDATE dbo.Courses
              SET CourseName = @CourseName,
                  Credits    = @Credits,
                  SchoolID   = @SchoolID
              WHERE CourseID = @CourseID;";

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


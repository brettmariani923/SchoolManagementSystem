using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Courses.Insert
{
    public sealed class InsertNewCourse : IDataExecute
    {
        private readonly Courses_Row _course;

        public InsertNewCourse(Courses_Row course)
        {
            _course = course ?? throw new ArgumentNullException(nameof(course));

            if (string.IsNullOrWhiteSpace(_course.CourseName))
                throw new ArgumentException("CourseName cannot be null or empty.", nameof(course));

            if (_course.Credits <= 0)
                throw new ArgumentOutOfRangeException(nameof(course.Credits), "Credits must be greater than zero.");

            if (_course.SchoolID <= 0)
                throw new ArgumentOutOfRangeException(nameof(course.SchoolID), "SchoolID must be greater than zero.");
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Courses (CourseName, Credits, SchoolID)" +
             "VALUES (@CourseName, @Credits, @SchoolID);";

        public object GetParameters() =>
            new
            {
                _course.CourseName,
                _course.Credits,
                _course.SchoolID
            };
    }
}

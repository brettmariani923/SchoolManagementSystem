using System;
using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Courses.Update
{
    public sealed class UpdateCourse : IDataExecute
    {
        private readonly Courses_Row _row;

        public UpdateCourse(Courses_Row row)
        {
            _row = row ?? throw new ArgumentNullException(nameof(row));
            if (_row.CourseID <= 0)
                throw new ArgumentException("CourseID must be a positive existing ID.", nameof(row));
        }

        public string GetSql() =>
            @"UPDATE dbo.Courses
              SET CourseName = @CourseName,
                  Credits    = @Credits,
                  SchoolID   = @SchoolID
              WHERE CourseID = @CourseID;";

        public object GetParameters() => new
        {
            CourseID = _row.CourseID,
            CourseName = _row.CourseName,
            Credits = _row.Credits,
            SchoolID = _row.SchoolID
        };
    }
}

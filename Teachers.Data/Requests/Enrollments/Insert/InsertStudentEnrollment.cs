using System;
using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Insert
{
    public sealed class InsertStudentEnrollment : IDataExecute
    {
        private readonly Enrollments_Row _row;

        public InsertStudentEnrollment(Enrollments_Row row)
        {
            _row = row ?? throw new ArgumentNullException(nameof(row));

            if (_row.StudentID <= 0)
                throw new ArgumentOutOfRangeException(nameof(row.StudentID), "StudentID must be positive.");
            if (_row.TeacherID <= 0)
                throw new ArgumentOutOfRangeException(nameof(row.TeacherID), "TeacherID must be positive.");
            if (_row.CourseID <= 0)
                throw new ArgumentOutOfRangeException(nameof(row.CourseID), "CourseID must be positive.");
            if (_row.SchoolID <= 0)
                throw new ArgumentOutOfRangeException(nameof(row.SchoolID), "SchoolID must be positive.");
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Enrollments (StudentID, TeacherID, CourseID, SchoolID)" +
             "VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);";

        public object GetParameters() => new
        {
            _row.StudentID,
            _row.TeacherID,
            _row.CourseID,
            _row.SchoolID
        };
    }
}

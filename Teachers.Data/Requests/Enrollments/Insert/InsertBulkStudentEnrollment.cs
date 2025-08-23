using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Insert
{
    public sealed class InsertBulkStudentEnrollment : IDataExecute
    {
        private readonly Enrollments_Row[] _rows;

        public InsertBulkStudentEnrollment(IEnumerable<Enrollments_Row> rows)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows));
            _rows = rows.ToArray();
            if (_rows.Length == 0) throw new ArgumentException("At least one enrollment is required.", nameof(rows));
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Enrollments (StudentID, TeacherID, CourseID, SchoolID)
          VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);";

        public object? GetParameters() =>
            _rows.Select(r => new { r.StudentID, r.TeacherID, r.CourseID, r.SchoolID });
    }

}

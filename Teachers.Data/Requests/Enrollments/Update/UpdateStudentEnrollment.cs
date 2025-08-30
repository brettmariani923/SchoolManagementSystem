using Teachers.Data.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Update
{
    public sealed class UpdateStudentEnrollment : IDataExecute
    {
        private readonly Enrollments_Row _row;

        public UpdateStudentEnrollment(Enrollments_Row row)
        {
            _row = row ?? throw new ArgumentNullException(nameof(row));
            if (_row.EnrollmentID <= 0)
                throw new ArgumentException("EnrollmentID must be a positive existing ID.", nameof(row));
        }

        public string GetSql() =>
            @"UPDATE dbo.Enrollments " +
            "SET StudentID = @StudentID, " +
            "TeacherID = @TeacherID, " +
            "CourseID  = @CourseID, " +
            "SchoolID  = @SchoolID " +
            "WHERE EnrollmentID = @EnrollmentID;";

        public object GetParameters() => new
        {
            EnrollmentID = _row.EnrollmentID,
            StudentID = _row.StudentID,
            TeacherID = _row.TeacherID,
            CourseID = _row.CourseID,
            SchoolID = _row.SchoolID
        };
    }
}

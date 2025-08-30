using Teachers.Data.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Update
{
    public class UpdateBulkEnrollments : IDataExecute
    {
        private readonly IEnumerable<Enrollments_Row> _enrollments;

        public UpdateBulkEnrollments(IEnumerable<Enrollments_Row> enrollments)
        {
            _enrollments = enrollments ?? throw new ArgumentNullException(nameof(enrollments));
            if (!_enrollments.Any())
                throw new ArgumentException("At least one enrollment is required.", nameof(enrollments));
        }

        public string GetSql() =>
        @"UPDATE dbo.Enrollments " +
         "SET StudentID = @StudentID, " +
         "TeacherID = @TeacherID, " +
         "CourseID  = @CourseID, " +
         "SchoolID  = @SchoolID " +
         "WHERE EnrollmentID = @EnrollmentID;";


        public object? GetParameters() =>
            _enrollments.Select(e => new
            {
                e.EnrollmentID,
                e.StudentID,
                e.TeacherID,
                e.CourseID,
                e.SchoolID
            });
    }
}

using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Enrollments.Update
{
    public class UpdateBulkEnrollments : IDataExecute
    {
        private readonly IEnumerable<Enrollments_Row> _enrollments;

        public UpdateBulkEnrollments(IEnumerable<Enrollments_Row> enrollments)
        {
            if (enrollments is null) throw new ArgumentNullException(nameof(enrollments));
            _enrollments = enrollments;
        }

        public string GetSql() =>
        "UPDATE dbo.Enrollments " +
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

using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Enrollments.Remove
{
    public class RemoveStudentEnrollment : IDataExecute
    {
        private readonly int _enrollmentID;

        public RemoveStudentEnrollment(int enrollmentID)
        {
            _enrollmentID = enrollmentID;
        }

        public string GetSql() => @"DELETE FROM dbo.Enrollments WHERE EnrollmentID = @EnrollmentID;";

        public object? GetParameters() => new { EnrollmentID = _enrollmentID };

    }
}

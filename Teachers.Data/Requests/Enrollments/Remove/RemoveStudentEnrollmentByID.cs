using Teachers.Data.Interfaces;

namespace Teachers.Data.Requests.Enrollments.Remove
{
    public class RemoveStudentEnrollmentByID : IDataExecute
    {
        private readonly int _enrollmentID;

        public RemoveStudentEnrollmentByID(int enrollmentID)
        {
            if (enrollmentID <= 0)
                throw new ArgumentOutOfRangeException(nameof(enrollmentID), "EnrollmentID must be positive.");
            _enrollmentID = enrollmentID;
        }

        public string GetSql() => @"DELETE FROM dbo.Enrollments WHERE EnrollmentID = @EnrollmentID;";

        public object? GetParameters() => new { EnrollmentID = _enrollmentID };

    }
}

using Teachers.Data.Interfaces;

namespace Teachers.Data.Requests.Enrollments.Remove
{
    public class RemoveBulkStudentEnrollments : IDataExecute
    {
        private readonly int[] _enrollmentIDs;

        public RemoveBulkStudentEnrollments(IEnumerable<int> enrollmentIDs)
        {
            if (enrollmentIDs is null) throw new ArgumentNullException(nameof(enrollmentIDs));
            _enrollmentIDs = enrollmentIDs.ToArray();
            if (_enrollmentIDs.Length == 0)
                throw new ArgumentException("At least one EnrollmentID is required.", nameof(enrollmentIDs));
        }

        public string GetSql() => @"DELETE FROM dbo.Enrollments WHERE EnrollmentID IN @EnrollmentIDs;";

        public object? GetParameters() => new { EnrollmentIDs = _enrollmentIDs };
    }
}

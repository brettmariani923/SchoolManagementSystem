using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Enrollments.Remove
{
    public class RemoveBulkStudentEnrollments : IDataExecute
    {
        private readonly int[] _studentIDs;

        public RemoveBulkStudentEnrollments(IEnumerable<int> studentIDs)
        {
            if (studentIDs is null) throw new ArgumentNullException(nameof(studentIDs));
            _studentIDs = studentIDs.Distinct().ToArray();
            if (_studentIDs.Length == 0)
                throw new ArgumentException("At least one StudentID is required.", nameof(studentIds));
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Enrollments
              WHERE StudentID IN @StudentIDs;";

        public object? GetParameters() => new { StudentIDs = _studentIDs };
    }
}

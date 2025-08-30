using Teachers.Data.Interfaces;

namespace Teachers.Data.Requests.Students.Remove
{
    public class RemoveBulkStudents : IDataExecute
    {
        private readonly int[] _studentIDs;

        public RemoveBulkStudents(IEnumerable<int> studentIDs)
        {
            if (studentIDs is null) throw new ArgumentNullException(nameof(studentIDs));
            _studentIDs = studentIDs.Distinct().ToArray();
            if (_studentIDs.Length == 0)
                throw new ArgumentException("At least one StudentID is required.", nameof(studentIDs));
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Students " +
             "WHERE StudentID IN @StudentIDs;";

        public object? GetParameters() => new { StudentIDs = _studentIDs };
    }
}

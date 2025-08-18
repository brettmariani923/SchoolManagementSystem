using System.Collections.Generic;
using System.Linq;
using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Students
{
    public class RemoveBulkStudents : IDataExecute
    {
        private readonly IEnumerable<int> _studentIDs;

        public RemoveBulkStudents(IEnumerable<int> studentIDs)
        {
            _studentIDs = studentIDs;
        }

        public string GetSql() =>
            @"DELETE FROM Students
              WHERE StudentID = @StudentID;";

        public object? GetParameters() =>
            _studentIDs.Select(id => new { StudentID = id });
    }
}

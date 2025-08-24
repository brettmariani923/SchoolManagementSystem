using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Students.Insert
{
    public class InsertBulkStudents : IDataExecute
    {
        private readonly IEnumerable<Students_Row> _students;

        public InsertBulkStudents(IEnumerable<Students_Row> students)
        {
            _students = students ?? throw new ArgumentNullException(nameof(students));
            if (!_students.Any()) throw new ArgumentException("At least one student is required.", nameof(students));
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Students (FirstName, LastName, [Year], SchoolID) " +
             "VALUES (@FirstName, @LastName, @Year, @SchoolID);";

        public object? GetParameters() =>
            _students.Select(s => new
            {
                s.FirstName,
                s.LastName,
                s.Year,
                s.SchoolID
            });
    }
}

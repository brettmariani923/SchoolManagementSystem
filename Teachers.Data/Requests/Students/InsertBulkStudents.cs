using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Students
{
    public class InsertBulkStudents : IDataExecute
    {
        private readonly IEnumerable<Students_DTO> _students;
        private readonly int _schoolID;

        public InsertBulkStudents(IEnumerable<Students_DTO> students, int schoolID)
        {
            _students = students ?? throw new ArgumentNullException(nameof(students));
            if (!_students.Any()) throw new ArgumentException("At least one student is required.", nameof(students));
            _schoolID = schoolID;
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Students (FirstName, LastName, [Year], SchoolID)
              VALUES (@FirstName, @LastName, @Year, @SchoolID);";

        public object? GetParameters() =>
            _students.Select(s => new
            {
                s.FirstName,
                s.LastName,
                Year = s.Year,
                SchoolID = _schoolID
            });
    }
}

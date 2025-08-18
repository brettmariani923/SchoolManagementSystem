using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Students
{
    public class InsertBulkStudents : IDataExecute
    {
        private readonly IEnumerable<(string FirstName, string LastName, int Year)> _students;
        private readonly int _schoolID;

        public InsertBulkStudents(IEnumerable<(string FirstName, string LastName, int Year)> students, int schoolID)
        {
            _students = students ?? throw new ArgumentNullException(nameof(students));
            _schoolID = schoolID;
        }

        public string GetSql() =>
            "INSERT INTO dbo.Students (FirstName, LastName, [Year], SchoolID) " +
            "VALUES (@FirstName, @LastName, @Year, @SchoolID);";

        public object GetParameters() =>
            _students.Select(s => new
            {
                s.FirstName,
                s.LastName,
                Year = s.Year,
                SchoolID = _schoolID
            });
    }
}

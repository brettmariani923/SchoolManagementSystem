namespace Teachers.Data.Requests.Students
{
    public class InsertBulkStudents
    {
        private readonly IEnumerable<(string FirstName, string LastName)> _students;
        private readonly int _schoolID;

        public InsertBulkStudents(IEnumerable<(string FirstName, string LastName)> students, int schoolID)
        {
            _students = students ?? throw new ArgumentNullException(nameof(students));
            _schoolID = schoolID;
        }

        public string GetSql() =>
            "INSERT INTO Students (FirstName, LastName, SchoolID) " +
            "VALUES (@FirstName, @LastName, @SchoolID);";

        public IEnumerable<object> GetParameters() =>
            _students.Select(s => new
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                SchoolID = _schoolID
            });
    }
}

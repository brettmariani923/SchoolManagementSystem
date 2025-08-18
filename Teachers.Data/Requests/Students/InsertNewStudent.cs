using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Students
{
    public class InsertNewStudent : IDataExecute
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly int _year;
        private readonly int _schoolID;

        public InsertNewStudent(string firstName, string lastName, int year, int schoolID)
        {
            _firstName = firstName;
            _lastName = lastName;
            _year = year;
            _schoolID = schoolID;
        }

        public string GetSql() =>
            "INSERT INTO dbo.Students (FirstName, LastName, [Year], SchoolID) " +
            "VALUES (@FirstName, @LastName, @Year, @SchoolID);";

        public object GetParameters() => new
        {
            FirstName = _firstName,
            LastName = _lastName,
            Year = _year,
            SchoolID = _schoolID
        };
    }
}

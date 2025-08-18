namespace Teachers.Data.Requests.Students
{
    public class InsertNewStudent
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly int _schoolID;

        public InsertNewStudent(string firstName, string lastName, int schoolID)
        {
            _firstName = firstName;
            _lastName = lastName;
            _schoolID = schoolID;
        }

        public string GetSql() =>
            "INSERT INTO Students (FirstName, LastName, SchoolID) " +
            "VALUES (@FirstName, @LastName, @SchoolID);";

        public object GetParameters() => new
        {
            FirstName = _firstName,
            LastName = _lastName,
            SchoolID = _schoolID
        };
    }
}

namespace Teachers.Data.Requests.Students
{
    public class InsertNewStudent
    {
        private readonly int _schoolID;
        private readonly int _firstName;
        private readonly int _lastName;

        public InsertNewStudent(int firstName, int lastName, int schoolID)
        {
            schoolID = _schoolID;
            firstName = _firstName;
            lastName = _lastName;
        }

        public string GetSql()
        {
            return @"
            INSERT INTO Students (FirstName, LastName, SchoolID) VALUES (@FirstName, @LastName, @SchoolID);";
        }

        public object? GetParameters()
        {
            return new { FirstName = _firstName, LastName = _lastName, SchoolID = _schoolID };
        }
    }
}

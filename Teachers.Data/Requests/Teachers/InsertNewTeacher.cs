namespace Teachers.Data.Requests.Teachers
{
    public class InsertNewTeacher
    {
        private readonly int _firstName;
        private readonly int _lastName;
        private readonly int _schoolID;

        public InsertNewTeacher(int firstName, int lastName, int schoolID)
        {
            firstName = _firstName;
            lastName = _lastName;
            schoolID = _schoolID;
        }

        public string GetSql()
        {
            return @"
            INSERT INTO Teachers (FirstName, LastName) VALUES (@Slot, @PokemonID);";
        }

        public object? GetParameters()
        {
            return new { FirstName = _firstName, LastName = _lastName, SchoolID = _schoolID };
        }
    }
}

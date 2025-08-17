namespace Teachers.Data.Requests.Courses
{
    public class InsertNewCourse
    {
        private readonly int _course;
        private readonly int _credits;

        public InsertNewCourse(int course, int credits)
        {
            course = _course;
            credits = _credits;
        }

        public string GetSql()
        {
            return @"
            INSERT INTO Courses (Course, Credits) VALUES (@Course, @Credits);";
        }

        public object GetParameters()
        {
            return new { Course = _course, Credits = _credits };
        }
    }
}

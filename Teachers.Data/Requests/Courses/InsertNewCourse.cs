namespace Teachers.Data.Requests.Courses
{
    public class InsertNewCourse
    {
        private readonly int _course;
        private readonly int _credits;

        public InsertNewCourse(int course, int credits)
        {
            _course = course;
            _credits = credits;
        }

        public string GetSql()
        {
            return @"
            INSERT INTO Courses (Course, Credits) VALUES @(Course, @Credits);";
        }

        public object GetParameters()
        {
            return new { Course = _course, Credits = _credits };
        }
    }
}

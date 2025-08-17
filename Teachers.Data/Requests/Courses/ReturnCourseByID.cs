namespace Teachers.Data.Requests.Courses
{
    public class ReturnCourseByID
    {
        private readonly int _courseID;
        private readonly int _courseName;
        private readonly int _credits;

        public ReturnCourseByID(int courseID, int courseName, int credits)
        {
            courseID = _courseID;
            courseName = _courseName;
            credits = _credits;

        }
        public string GetSql()
        {
            return "SELECT Course FROM SchoolSystem WHERE CourseID = @CourseID;";
        }

        public object? GetParameters()
        {
            return new { CourseID = _courseID };
        }
    }
}

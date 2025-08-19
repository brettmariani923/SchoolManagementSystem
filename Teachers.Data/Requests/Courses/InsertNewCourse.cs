using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Courses
{
    public class InsertNewCourse : IDataExecute
    {
        private readonly string _courseName;
        private readonly int _credits;
        private readonly int _schoolID;

        public InsertNewCourse(string courseName, int credits, int schoolID)
        {
            _courseName = courseName;
            _credits = credits;
            _schoolID = schoolID;
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Courses (CourseName, Credits, SchoolID)" +
              "VALUES (@CourseName, @Credits, @SchoolID);";

        public object GetParameters() =>
            new { CourseName = _courseName, Credits = _credits, SchoolID = _schoolID };
    }
}

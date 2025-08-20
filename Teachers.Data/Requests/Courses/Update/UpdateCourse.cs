using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Courses.Update
{
    public class UpdateCourse : IDataExecute
    {
        private readonly int _courseID;
        private readonly string _courseName;
        private readonly int _credits;
        private readonly int _schoolID;

        public UpdateCourse(int courseID, string courseName, int credits, int schoolID)
        {
            _courseID = courseID;
            _courseName = courseName;
            _credits = credits;
            _schoolID = schoolID;
        }

        public string GetSql() =>
              @"UPDATE dbo.Courses" +
              "SET CourseName = @CourseName," +
                  "Credits = @Credits," +
                  "SchoolID = @SchoolID" +
              "WHERE CourseID = @CourseID;";

        public object GetParameters() => new
        {
            CourseID = _courseID,
            CourseName = _courseName,
            Credits = _credits,
            SchoolID = _schoolID
        };
    }
}

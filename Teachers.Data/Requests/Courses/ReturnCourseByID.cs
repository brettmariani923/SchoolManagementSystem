using Teachers.Domain.Interfaces;
using Teachers.Data.DTO; 

namespace Teachers.Data.Requests.Courses
{
    public class ReturnCourseByID : IDataFetch<Courses_DTO>
    {
        private readonly int _courseID;

        public ReturnCourseByID(int courseID)
        {
            _courseID = courseID;
        }

        public string GetSql() =>
            @"SELECT CourseID, CourseName, Credits, SchoolID
              FROM dbo.Courses
              WHERE CourseID = @CourseID;";

        public object GetParameters() => new { CourseID = _courseID };
    }
}

using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Courses.Return
{
    public class ReturnAllCourses : IDataFetchList<Courses_DTO>
    {
        public string GetSql() =>
            @"SELECT CourseID, CourseName, Credits, SchoolID
              FROM dbo.Courses;";

        public object? GetParameters() => null;
    }
}

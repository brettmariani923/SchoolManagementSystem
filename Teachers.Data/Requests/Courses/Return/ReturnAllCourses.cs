using Teachers.Data.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Courses.Return
{
    public class ReturnAllCourses : IDataFetchList<Courses_Row>
    {
        public string GetSql() =>
            @"SELECT CourseID, CourseName, Credits, SchoolID " +
             "FROM dbo.Courses;";

        public object? GetParameters() => null;
    }
}

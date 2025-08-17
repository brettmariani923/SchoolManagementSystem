using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Courses
{
    public class ReturnAllCourses : IDataFetchList<Courses_DTO>
    {
       public string GetSql()
        {
            return "SELECT Courses FROM SchoolSystem;";
        }

        public object? GetParameters()
        {
            return null;
        }
    }
}

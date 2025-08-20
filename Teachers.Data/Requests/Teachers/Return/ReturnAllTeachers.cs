using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Teachers.Return
{
    public class ReturnAllTeachers : IDataFetchList<Teachers_DTO>
    {
        public string GetSql() =>
            @"SELECT TeacherID, FirstName, LastName, SchoolID" +
             "FROM dbo.Teachers;";

        public object? GetParameters() => null;
    }
}

using Teachers.Data.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Teachers.Return
{
    public class ReturnAllTeachers : IDataFetchList<Teachers_Row>
    {
        public string GetSql() =>
            @"SELECT TeacherID, FirstName, LastName, SchoolID " +
             "FROM dbo.Teachers;";

        public object? GetParameters() => null;
    }
}

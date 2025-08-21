using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Students.Return
{
    public class ReturnAllStudents : IDataFetchList<Students_Row>
    {
        public string GetSql() =>
            @"SELECT StudentID, FirstName, LastName, [Year], SchoolID" +
             "FROM dbo.Students;";

        public object? GetParameters() => null;
    }
}

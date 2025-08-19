using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Students.Return
{
    public class ReturnAllStudents : IDataFetchList<Students_DTO>
    {
        public string GetSql() =>
            @"SELECT StudentID, FirstName, LastName, [Year], SchoolID
              FROM dbo.Students;";

        public object? GetParameters() => null;
    }
}

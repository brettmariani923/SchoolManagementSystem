using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public class ReturnAllEnrollments : IDataFetchList<Enrollments_Row>
    {
        public string GetSql() => "SELECT * FROM dbo.Enrollments;";

        public object? GetParameters() => null;
    }
}

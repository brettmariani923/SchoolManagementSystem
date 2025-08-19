using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public class ReturnAllEnrollments : IDataFetchList<Enrollments_DTO>
    {
        public string GetSql() => @"SELECT * FROM ENROLLMENTS;";

        public object? GetParameters() => null;
    }
}

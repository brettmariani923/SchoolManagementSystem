using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Students.Return
{
    public class ReturnStudentById : IDataFetch<Students_Row>
    {
        private readonly int _studentID;

        public ReturnStudentById(int studentID)
        {
            _studentID = studentID;
        }

        public string GetSql() =>
            @"SELECT StudentID, FirstName, LastName, [Year], SchoolID" +
             "FROM dbo.Students" +
             "WHERE StudentID = @StudentID;";

        public object GetParameters() => new { StudentID = _studentID };
    }
}

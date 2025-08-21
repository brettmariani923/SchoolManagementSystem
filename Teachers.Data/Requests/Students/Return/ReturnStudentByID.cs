using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Students.Return
{
    public class ReturnStudentByID : IDataFetch<Students_Row>
    {
        private readonly int _studentID;

        public ReturnStudentByID(int studentID)
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

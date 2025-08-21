using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Students.Remove
{
    public class RemoveStudentByID : IDataExecute
    {
        private readonly int _studentID;

        public RemoveStudentByID(int studentID)
        {
            _studentID = studentID;
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Students" +
             "WHERE StudentID = @StudentID;";

        public object? GetParameters() => new { StudentID = _studentID };

    }
}

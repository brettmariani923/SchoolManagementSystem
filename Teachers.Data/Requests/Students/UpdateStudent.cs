using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Students
{
    public class UpdateStudent : IDataExecute
    {
        private readonly int _studentID;

        public UpdateStudent(int studentID)
        {
            _studentID = studentID;
        }

        public string GetSql() =>
             @"UPDATE dbo.Students
              SET StudentID = @StudentID,
                  FirstName = @FirstName,
                  LastName = @LastName,
                  SchoolID = @SchoolID
              WHERE StudentID = @StudentID;";

        public object? GetParameters() => new { StudentID = _studentID };

    }
}

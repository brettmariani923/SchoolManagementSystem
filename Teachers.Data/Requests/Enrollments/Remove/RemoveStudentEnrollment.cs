using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Enrollments.Remove
{
    public class RemoveStudentEnrollment : IDataExecute
    {
        private readonly int _studentID;

        public RemoveStudentEnrollment(int studentID)
        {
            _studentID = studentID;
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Enrollments
              WHERE StudentID = @StudentID;";

        public object? GetParameters() => new { StudentID = _studentID };

    }
}

using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Students
{
    public class UpdateStudent : IDataExecute
    {
        private readonly Students_DTO _student;

        public UpdateStudent(Students_DTO student)
        {
            _student = student; 
        }

        public string GetSql() =>
            @"UPDATE Students
              SET FirstName = @FirstName,
                  LastName  = @LastName,
                  [Year]    = @Year,
                  SchoolID  = @SchoolID
              WHERE StudentID = @StudentID;";

        public object? GetParameters() => new
        {
            _student.StudentID,
            _student.FirstName,
            _student.LastName,
            _student.Year,
            _student.SchoolID
        };
    }
}

using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Teachers
{
    public class UpdateBulkTeachers : IDataExecute
    {
        private readonly IEnumerable<Teachers_DTO> _teachers;

        public UpdateBulkTeachers(IEnumerable<Teachers_DTO> teachers)
        {
            _teachers = teachers ?? throw new ArgumentNullException(nameof(teachers));
            if (!_teachers.Any())
                throw new ArgumentException("At least one teacher is required.", nameof(teachers));
        }

        public string GetSql() =>
            @"UPDATE dbo.Teachers
              SET FirstName = @FirstName,
                  LastName  = @LastName,
                  SchoolID  = @SchoolID
              WHERE TeacherID = @TeacherID;";

        public object? GetParameters() =>
            _teachers.Select(t => new
            {
                t.TeacherID, 
                t.FirstName,
                t.LastName,
                t.SchoolID
            });
    }
}

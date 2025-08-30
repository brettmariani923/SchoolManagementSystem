using Teachers.Data.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Teachers.Update
{
    public class UpdateBulkTeachers : IDataExecute
    {
        private readonly IEnumerable<Teachers_Row> _teachers;

        public UpdateBulkTeachers(IEnumerable<Teachers_Row> teachers)
        {
            _teachers = teachers ?? throw new ArgumentNullException(nameof(teachers));
            if (!_teachers.Any())
                throw new ArgumentException("At least one teacher is required.", nameof(teachers));
        }

        public string GetSql() =>
        @"UPDATE dbo.Teachers
          SET FirstName = @FirstName,
              LastName = @LastName,
              SchoolID = @SchoolID
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

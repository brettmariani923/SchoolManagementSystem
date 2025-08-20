using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Teachers.Insert
{
    public class InsertBulkNewTeachers : IDataExecute
    {
        private readonly IEnumerable<Teachers_DTO> _teachers;

        public InsertBulkNewTeachers(IEnumerable<Teachers_DTO> teachers)
        {
            _teachers = teachers ?? throw new ArgumentNullException(nameof(teachers));
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Teachers (TeacherID, FirstName, LastName, SchoolID)" +
             "VALUES (@TeacherID, @FirstName, @LastName, @SchoolID);";

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

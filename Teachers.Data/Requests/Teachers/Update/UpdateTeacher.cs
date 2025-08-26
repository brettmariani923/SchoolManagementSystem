using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Teachers.Update
{
    public sealed class UpdateTeacher : IDataExecute
    {
        private readonly Teachers_Row _row;

        public UpdateTeacher(Teachers_Row row)
        {
            _row = row ?? throw new ArgumentNullException(nameof(row));
            if (_row.TeacherID <= 0)
                throw new ArgumentException("TeacherID must be a positive existing ID.", nameof(row));
        }

        public string GetSql() =>
            @"UPDATE dbo.Teachers
              SET FirstName = @FirstName,
                  LastName  = @LastName,
                  SchoolID  = @SchoolID
              WHERE TeacherID = @TeacherID;";

        public object GetParameters() => new
        {
            TeacherID = _row.TeacherID,
            FirstName = _row.FirstName,
            LastName = _row.LastName,
            SchoolID = _row.SchoolID
        };
    }
}

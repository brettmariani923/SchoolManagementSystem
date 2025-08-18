using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Teachers
{
    public class RemoveTeacher : IDataExecute
    {
        private readonly int _teacherID;

        public RemoveTeacher(int teacherID)
        {
            _teacherID = teacherID;
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Teachers
              WHERE TeacherID = @TeacherID;";

        public object? GetParameters() => new { TeacherID = _teacherID };
    }
}

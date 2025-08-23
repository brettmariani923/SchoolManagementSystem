using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Teachers.Remove
{
    public class RemoveTeacherByID : IDataExecute
    {
        private readonly int _teacherID;

        public RemoveTeacherByID(int teacherID)
        {
            _teacherID = teacherID;
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Teachers" +
             "WHERE TeacherID = @TeacherID;";

        public object? GetParameters() => new { TeacherID = _teacherID };
    }
}

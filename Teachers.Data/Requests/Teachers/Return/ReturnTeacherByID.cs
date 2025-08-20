using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Teachers.Return
{
    public class ReturnTeacherByID : IDataFetch<Teachers_DTO>
    {
        private readonly int _teacherID;

        public ReturnTeacherByID(int teacherID)
        {
            _teacherID = teacherID;
        }

        public string GetSql() =>
            @"SELECT TeacherID, FirstName, LastName, SchoolID" +
             "FROM dbo.Teachers" +
             "WHERE TeacherID = @TeacherID;";

        public object GetParameters() => new { TeacherID = _teacherID };
    }
}

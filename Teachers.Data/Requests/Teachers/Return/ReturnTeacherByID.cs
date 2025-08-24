using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Teachers.Return
{
    public class ReturnTeacherByID : IDataFetch<Teachers_Row>
    {
        private readonly int _teacherID;

        public ReturnTeacherByID(int teacherID)
        {
            if (teacherID <= 0)
                throw new ArgumentOutOfRangeException(nameof(teacherID), "StudentID must be positive.");

            _teacherID = teacherID;
        }

        public string GetSql() =>
            @"SELECT TeacherID, FirstName, LastName, SchoolID " +
             "FROM dbo.Teachers " +
             "WHERE TeacherID = @TeacherID;";

        public object GetParameters() => new { TeacherID = _teacherID };
    }
}

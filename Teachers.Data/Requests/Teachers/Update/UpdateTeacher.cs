using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Teachers.Update
{
    public class UpdateTeacher : IDataExecute
    {
        private readonly int _teacherID;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly int _schoolID;

        public UpdateTeacher(int teacherID, string firstName, string lastName, int schoolID)
        {
            _teacherID = teacherID;
            _firstName = firstName;
            _lastName = lastName;
            _schoolID = schoolID;
        }

        public string GetSql() =>
            @"UPDATE dbo.Teachers" +
              "SET TeacherID = @TeacherID," +
                  "FirstName = @FirstName," +
                  "LastName = @LastName," +
                  "SchoolID = @SchoolID" +
              "WHERE TeacherID = @TeacherID;";

        public object? GetParameters() => new
        {
            TeacherID = _teacherID,
            FirstName = _firstName,
            LastName = _lastName,
            SchoolID = _schoolID
        };
    }
}

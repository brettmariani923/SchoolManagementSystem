using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Enrollments.Insert
{
    public class InsertBulkStudentEnrollment : IDataExecute
    {
        private readonly IEnumerable<int> _studentIDs;
        private readonly int _teacherID;
        private readonly int _courseID;
        private readonly int _schoolID;

        public InsertBulkStudentEnrollment(IEnumerable<int> studentIDs, int teacherID, int courseID, int schoolID)
        {
            _studentIDs = studentIDs ?? throw new ArgumentNullException(nameof(studentIDs));
            _teacherID = teacherID;
            _courseID = courseID;
            _schoolID = schoolID;
        }

        public string GetSql() =>
            "INSERT INTO dbo.Enrollments (StudentID, TeacherID, CourseID, SchoolID) " +
            "VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);";

        public object GetParameters() =>
            _studentIDs.Select(id => new
            {
                StudentID = id,
                TeacherID = _teacherID,
                CourseID = _courseID,
                SchoolID = _schoolID
            });
    }
}

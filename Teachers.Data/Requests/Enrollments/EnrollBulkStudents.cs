namespace Teachers.Data.Requests.Enrollments
{
    public class EnrollBulkStudents
    {
        private readonly IEnumerable<int> _studentID;
        private readonly int _teacherID;
        private readonly int _courseID;
        private readonly int _schoolID;

        public EnrollBulkStudents(IEnumerable<int> studentID, int teacherID, int courseID, int schoolID)
        {
            studentID = _studentID;
            teacherID = _teacherID;
            courseID = _courseID;
            schoolID = _schoolID;
        }

        public string GetSql() =>
            "INSERT INTO Enrollments (StudentID, TeacherID, CourseID, SchoolID) " +
            "VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);";

        public IEnumerable<object> GetParameters() =>
            _studentID.Select(id => new
            {
                StudentID = id,
                TeacherID = _teacherID,
                CourseID = _courseID,
                SchoolID = _schoolID
            });
    }
  
}

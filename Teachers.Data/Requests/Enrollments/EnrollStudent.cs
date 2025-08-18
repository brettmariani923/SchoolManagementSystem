namespace Teachers.Data.Requests.Enrollments
{
    public class EnrollStudent
    {
        private readonly int _studentID;
        private readonly int _teacherID;
        private readonly int _courseID;
        private readonly int _schoolID;

        public EnrollStudent(int studentID, int teacherID, int courseID, int schoolID)
        {
            _studentID = studentID;
            _teacherID = teacherID;
            _courseID = courseID;
            _schoolID = schoolID;
        }

        public string GetSql() =>
            "INSERT INTO Enrollments (StudentID, TeacherID, CourseID, SchoolID) " +
            "VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);";

        public object GetParameters() => new
        {
            StudentID = _studentID,
            TeacherID = _teacherID,
            CourseID = _courseID,
            SchoolID = _schoolID
        };
    }
}

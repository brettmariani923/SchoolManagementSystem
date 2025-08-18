namespace Teachers.Data.Requests.Enrollments
{
    public class EnrollBulkStudents
    {
        private readonly IEnumerable<int> _studentIds;
        private readonly int _teacherID;
        private readonly int _courseID;
        private readonly int _schoolID;

        public EnrollBulkStudents(IEnumerable<int> studentIds, int teacherID, int courseID, int schoolID)
        {
            _studentIds = studentIds ?? throw new ArgumentNullException(nameof(studentIds));
            _teacherID = teacherID;
            _courseID = courseID;
            _schoolID = schoolID;
        }

        public string GetSql() =>
            "INSERT INTO dbo.Enrollments (StudentID, TeacherID, CourseID, SchoolID) " +
            "VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);";

        public IEnumerable<object> GetParameters() =>
            _studentIds.Select(id => new
            {
                StudentID = id,
                TeacherID = _teacherID,
                CourseID = _courseID,
                SchoolID = _schoolID
            });
    }
}

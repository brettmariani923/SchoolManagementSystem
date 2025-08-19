using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Enrollments.Update
{
    public class UpdateStudentEnrollment : IDataExecute
    {
        private readonly int _enrollmentID;
        private readonly int _studentID;
        private readonly int _teacherID;
        private readonly int _courseID;
        private readonly int _schoolID;

        public UpdateStudentEnrollment(int enrollmentID, int studentID, int teacherID, int courseID, int schoolID)
        {
            _enrollmentID = enrollmentID;
            _studentID = studentID;
            _teacherID = teacherID;
            _courseID = courseID;
            _schoolID = schoolID;
        }

        public string GetSql() =>
            @"UPDATE dbo.Enrollments
              SET StudentID = @StudentID,
                  TeacherID = @TeacherID,
                  CourseID  = @CourseID,
                  SchoolID  = @SchoolID
              WHERE EnrollmentID = @EnrollmentID;";

        public object GetParameters() => new
        {
            EnrollmentID = _enrollmentID,
            StudentID = _studentID,
            TeacherID = _teacherID,
            CourseID = _courseID,
            SchoolID = _schoolID
        };
    }
}

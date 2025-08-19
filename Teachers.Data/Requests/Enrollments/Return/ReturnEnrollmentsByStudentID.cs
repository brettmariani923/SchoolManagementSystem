using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public class ReturnEnrollmentsByStudentID : IDataFetchList<Enrollments_DTO>
    {
        private readonly int _studentID;

        public ReturnEnrollmentsByStudentID(int studentID)
        {
            _studentID = studentID;
        }

        public string GetSql() =>
            @"SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID
                FROM dbo.Enrollments
                WHERE StudentID = @StudentID
                ORDER BY EnrollmentID;";

        public object GetParameters() => new { StudentID = _studentID };
    }
}

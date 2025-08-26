using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public sealed class ReturnEnrollmentsByStudentID : IDataFetchList<Enrollments_Row>
    {
        private readonly int _studentID;

        public ReturnEnrollmentsByStudentID(int studentID)
        {
            if (studentID <= 0)
                throw new ArgumentOutOfRangeException(nameof(studentID), "StudentID must be positive.");

            _studentID = studentID;
        }

        public string GetSql() =>
            @"SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID " +
              "FROM dbo.Enrollments " +
              "WHERE StudentID = @StudentID;";

        public object GetParameters() => new { StudentID = _studentID };
    }
}

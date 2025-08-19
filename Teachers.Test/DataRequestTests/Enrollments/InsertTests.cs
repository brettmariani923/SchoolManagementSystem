using Teachers.Data.Requests.Enrollments.Insert;

namespace Teachers.Test.DataRequestTests.Enrollments
{
    public class InsertTests
    {
        private const string ExpectedSql =
            "INSERT INTO Enrollments (StudentID, TeacherID, CourseID, SchoolID) " +
            "VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);";

        [Fact]
        public void InsertEnrollment_GetSql_ReturnsExpectedInsertStatement()
        {
            // Arrange
            var req = new InsertStudentEnrollment(studentID: 101, teacherID: 7, courseID: 55, schoolID: 3);

            // Act
            var sql = req.GetSql();

            // Assert
            Assert.Equal(ExpectedSql, sql);
        }

        [Fact]
        public void InsertEnrollment_GetParameters_Projects_AllFields()
        {
            // Arrange
            var req = new InsertStudentEnrollment(studentID: 101, teacherID: 7, courseID: 55, schoolID: 3);

            // Act
            var p = req.GetParameters();

            // Assert
            var studentID = p!.GetType().GetProperty("StudentID")!.GetValue(p);
            var teacherID = p.GetType().GetProperty("TeacherID")!.GetValue(p);
            var courseID = p.GetType().GetProperty("CourseID")!.GetValue(p);
            var schoolID = p.GetType().GetProperty("SchoolID")!.GetValue(p);

            Assert.Equal(101, studentID);
            Assert.Equal(7, teacherID);
            Assert.Equal(55, courseID);
            Assert.Equal(3, schoolID);
        }

        [Fact]
        public void InsertBulkEnrollments_GetSql_ReturnsExpectedInsertStatement()
        {
            // Arrange
            var req = new InsertBulkStudentEnrollment(new[] { 101, 102 }, teacherID: 7, courseID: 55, schoolID: 3);

            // Act
            var sql = req.GetSql();

            // Assert
            Assert.Equal(ExpectedSql, sql);
        }

        [Fact]
        public void InsertBulkEnrollments_NullStudents_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int>? students = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new InsertBulkStudentEnrollment(students!, teacherID: 7, courseID: 55, schoolID: 3));
        }

        [Fact]
        public void InsertBulkEnrollments_EmptyStudents_ThrowsArgumentException()
        {
            // Arrange
            var empty = Array.Empty<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new InsertBulkStudentEnrollment(empty, teacherID: 7, courseID: 55, schoolID: 3));
        }
    }
}

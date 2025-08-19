using Teachers.Data.Requests.Enrollments.Remove;

namespace Teachers.Test.DataRequestTests.Enrollments
{
    // Adjust namespaces/type names below if your request classes live elsewhere.
    public class RemoveTests
    {
        [Fact]
        public void RemoveEnrollment_GetSql_DeletesByEnrollmentID()
        {
            // Arrange
            var req = new RemoveStudentEnrollment(42);

            // Act
            var sql = req.GetSql();

            // Assert
            Assert.Equal(
                "DELETE FROM dbo.Enrollments WHERE EnrollmentID = @EnrollmentID;",
                sql.Trim());
        }

        [Fact]
        public void RemoveEnrollment_GetParameters_ProjectsEnrollmentID()
        {
            // Arrange
            
            var req = new RemoveStudentEnrollment(42);

            // Act
            var p = req.GetParameters();

            // Assert
            Assert.NotNull(p);
            var enrollmentIdProperty = p!.GetType().GetProperty("EnrollmentID");
            Assert.NotNull(enrollmentIdProperty);
            Assert.Equal(42, (int)enrollmentIdProperty!.GetValue(p)!);
        }

        [Fact]
        public void RemoveBulkEnrollments_GetSql_DeletesByEnrollmentID()
        {
            // Arrange
            var ids = new[] { 101, 102, 103 };
            var req = new RemoveBulkStudentEnrollments(ids);

            // Act
            var sql = req.GetSql();

            // Assert
            Assert.Equal(
                "DELETE FROM dbo.Enrollments WHERE EnrollmentID = @EnrollmentID;",
                sql.Trim());
        }

        [Fact]
        public void RemoveBulkEnrollments_Ctor_NullIds_Throws()
        {
            // Arrange
            IEnumerable<int>? ids = null;

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => new RemoveBulkStudentEnrollments(ids!));
        }

        [Fact]
        public void RemoveBulkEnrollments_Ctor_EmptyIds_Throws()
        {
            // Arrange
            var ids = Array.Empty<int>();

            // Act + Assert
            Assert.Throws<ArgumentException>(() => new RemoveBulkStudentEnrollments(ids));
        }
    }
}

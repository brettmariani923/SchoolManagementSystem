using Teachers.Data.Requests.Enrollments.Update; // <-- needed for UpdateStudentEnrollment
using Teachers.Data.DTO;


namespace Teachers.Test.DataRequestTests.Enrollments
{
    public class UpdateTests
    {
        [Fact]
        public void UpdateStudentEnrollment_GetSql()
        {        

            var req = new UpdateStudentEnrollment(1, 1, 1, 1, 1);

            Assert.Equal(
                "UPDATE dbo.Enrollments " +
                "SET StudentID = @StudentID, " +
                "TeacherID = @TeacherID, " +
                "CourseID  = @CourseID, " +
                "SchoolID  = @SchoolID " +
                "WHERE EnrollmentID = @EnrollmentID;",
                req.GetSql());
        }

        [Fact]
        public void GetSql_ReturnsExpected()
        {
            var req = new UpdateStudentEnrollment(1, 1, 1, 1, 1);

            const string expected =
                "UPDATE dbo.Enrollments " +
                "SET StudentID = @StudentID, " +
                "TeacherID = @TeacherID, " +
                "CourseID  = @CourseID, " +
                "SchoolID  = @SchoolID " +
                "WHERE EnrollmentID = @EnrollmentID;";

            Assert.Equal(expected, req.GetSql());
        }

        [Fact]
        public void GetSql_ReturnsExpectedUpdateStatement()
        {
            var items = new[]
            {
                new Enrollments_DTO { EnrollmentID = 1, StudentID = 10, TeacherID = 20, CourseID = 30, SchoolID = 40 }
            };

            var req = new UpdateBulkEnrollments(items);

            Assert.Equal(
                "UPDATE dbo.Enrollments " +
                "SET StudentID = @StudentID, " +
                "TeacherID = @TeacherID, " +
                "CourseID  = @CourseID, " +
                "SchoolID  = @SchoolID " +
                "WHERE EnrollmentID = @EnrollmentID;",
                req.GetSql());
        }

        [Fact]
        public void GetParameters_OneObjectPerItem_WithExpectedFields()
        {
            var items = new[]
            {
                new Enrollments_DTO { EnrollmentID = 1, StudentID = 10, TeacherID = 20, CourseID = 30, SchoolID = 40 },
                new Enrollments_DTO { EnrollmentID = 2, StudentID = 11, TeacherID = 21, CourseID = 31, SchoolID = 41 },
            };
            var req = new UpdateBulkEnrollments(items);
            Assert.Equal(
                items.Length,
                (req.GetParameters() as IEnumerable<object>)?.Count() );
        }

        [Fact]
        public void UpdateBulkEnrollments_GetSql()
        {
            var items = new[]
            {
                new Enrollments_DTO { EnrollmentID = 1, StudentID = 10, TeacherID = 20, CourseID = 30, SchoolID = 40 },
                new Enrollments_DTO { EnrollmentID = 2, StudentID = 11, TeacherID = 21, CourseID = 31, SchoolID = 41 },
            };
            var req = new UpdateBulkEnrollments(items);

            Assert.Equal(
                "UPDATE dbo.Enrollments " +
                "SET StudentID = @StudentID, " +
                "TeacherID = @TeacherID, " +
                "CourseID  = @CourseID, " +
                "SchoolID  = @SchoolID " +
                "WHERE EnrollmentID = @EnrollmentID;",
                req.GetSql());
        }
    }
}

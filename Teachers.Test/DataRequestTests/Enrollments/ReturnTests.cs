using Teachers.Data.Requests.Enrollments.Return;

namespace Teachers.Test.DataRequestTests.Enrollments
{
    public class ReturnTests
    {
        [Fact]
        public void ReturnAllEnrollments_GetSql_SelectsAll()
        {
            var req = new ReturnAllEnrollments();

            Assert.Equal(
                "SELECT * FROM dbo.Enrollments;",
                req.GetSql().Trim());
        }

        [Fact]
        public void ReturnAllEnrollments_GetParameters_IsNull()
        {
            var req = new ReturnAllEnrollments();

            Assert.Null(req.GetParameters());
        }

        [Fact]
        public void ReturnEnrollmentByCourseID_GetSql_SelectsByCourseID()
        {
            var req = new ReturnEnrollmentsByCourseID(55);

            Assert.Equal(
                "SELECT * FROM dbo.Enrollments WHERE CourseID = @CourseID;",
                req.GetSql().Trim());
        }

        [Fact]
        public void ReturnEnrollmentByCourseID_GetParameters_ProjectsCourseID()
        {
            var courseId = 55;
            var req = new ReturnEnrollmentsByCourseID(courseId);

            dynamic p = req.GetParameters()!;
            Assert.Equal(courseId, (int)p.CourseID);
        }

        [Fact]
        public void ReturnEnrollmentsByEnrollmentID_GetSql_SelectsByEnrollmentID()
        {
            var req = new ReturnEnrollmentsByEnrollmentID(42);

            Assert.Equal(
                "SELECT * FROM dbo.Enrollments WHERE EnrollmentID = @EnrollmentID;",
                req.GetSql().Trim());
        }

        [Fact]
        public void ReturnEnrollmentsByStudentID_GetSql_SelectsByStudentID()
        {
            var req = new ReturnEnrollmentsByStudentID(101);

            Assert.Equal(
                "SELECT * FROM dbo.Enrollments WHERE StudentID = @StudentID;",
                req.GetSql().Trim());
        }

    }
}

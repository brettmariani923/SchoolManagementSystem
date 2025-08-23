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
            "SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID" +
             "FROM dbo.Enrollments;",

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
                "SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID" +
                 "FROM dbo.Enrollments" +
                 "WHERE CourseID = @CourseID;",
                req.GetSql().Trim());
        }

        [Fact]
        public void ReturnEnrollmentByCourseID_GetParameters_ProjectsCourseID()
        {
            var req = new ReturnEnrollmentsByCourseID(1);
            var p = req.GetParameters();
            var courseId = (int)p!.GetType().GetProperty("CourseID")!.GetValue(p)!;
            Assert.Equal(1, courseId);
        }

        [Fact]
        public void ReturnEnrollmentsByEnrollmentID_GetSql_SelectsByEnrollmentID()
        {
            var req = new ReturnEnrollmentsByEnrollmentID(42);

            Assert.Equal(
                "SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID" +
                 "FROM dbo.Enrollments" +
                 "WHERE EnrollmentID = @EnrollmentID;",

                req.GetSql().Trim());
        }

        [Fact]
        public void ReturnEnrollmentsByStudentID_GetSql_SelectsByStudentID()
        {
            var req = new ReturnEnrollmentsByStudentID(101);

            Assert.Equal(
                "SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID" +
                 "FROM dbo.Enrollments" +
                 "WHERE StudentID = @StudentID;",

                req.GetSql().Trim());
        }

        [Fact]
        public void ReturnEnrollmentsByTeacherID_GetSql_SelectsByTeacherID()
        {
            var req = new ReturnEnrollmentsByTeacherID(101);

            Assert.Equal(
                "SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID" +
                 "FROM dbo.Enrollments" +
                 "WHERE TeacherID = @TeacherID;",

                req.GetSql().Trim());
        }
    }
}

using Xunit;
using Teachers.Data.Requests.Enrollments.Insert;

namespace Teachers.Test.DataRequestTests.Enrollments
{
    public class InsertTests
    {
        [Fact]
        public void InsertEnrollment_GetSql()
        {
            var req = new InsertStudentEnrollment(101, 7, 55, 3);

            Assert.Equal(
                "INSERT INTO dbo.Enrollments (StudentID, TeacherID, CourseID, SchoolID) " +
                "VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);",
                req.GetSql());
        }

        [Fact]
        public void InsertEnrollment_GetParameters()
        {
            var req = new InsertStudentEnrollment(101, 7, 55, 3);

            dynamic p = req.GetParameters()!;
            Assert.Equal(101, (int)p.StudentID);
            Assert.Equal(7, (int)p.TeacherID);
            Assert.Equal(55, (int)p.CourseID);
            Assert.Equal(3, (int)p.SchoolID);
        }

        [Fact]
        public void InsertBulkEnrollments_GetSql()
        {
            var req = new InsertBulkStudentEnrollment(new[] { 101, 102 }, 7, 55, 3);

            Assert.Equal(
                "INSERT INTO dbo.Enrollments (StudentID, TeacherID, CourseID, SchoolID) " +
                "VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);",
                req.GetSql());
        }

        [Fact]
        public void InsertBulkEnrollments_GetParameters()
        {
            var req = new InsertBulkStudentEnrollment(new[] { 101, 102 }, 7, 55, 3);

            var list = req.GetParameters() as IEnumerable<dynamic>;
            Assert.NotNull(list);

            var first = list!.First();
            Assert.Equal(101, (int)first.StudentID);
            Assert.Equal(7, (int)first.TeacherID);
            Assert.Equal(55, (int)first.CourseID);
            Assert.Equal(3, (int)first.SchoolID);
        }
    }
}

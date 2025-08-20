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

            var p = req.GetParameters()!;
            var t = p.GetType();

            Assert.Equal(101, (int)t.GetProperty("StudentID")!.GetValue(p)!);
            Assert.Equal(7, (int)t.GetProperty("TeacherID")!.GetValue(p)!);
            Assert.Equal(55, (int)t.GetProperty("CourseID")!.GetValue(p)!);
            Assert.Equal(3, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
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

            var list = ((IEnumerable<object>)req.GetParameters()!).ToList();

            var first = list[0];
            var t = first.GetType();

            Assert.Equal(101, (int)t.GetProperty("StudentID")!.GetValue(first)!);
            Assert.Equal(7, (int)t.GetProperty("TeacherID")!.GetValue(first)!);
            Assert.Equal(55, (int)t.GetProperty("CourseID")!.GetValue(first)!);
            Assert.Equal(3, (int)t.GetProperty("SchoolID")!.GetValue(first)!);
        }

    }
}

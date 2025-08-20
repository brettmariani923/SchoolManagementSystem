using System.Linq;
using Xunit;
using Teachers.Data.Requests.Enrollments.Remove;

namespace Teachers.Test.DataRequestTests.Enrollments
{
    public class RemoveTests
    {
        [Fact]
        public void RemoveEnrollment_GetSql()
        {
            var req = new RemoveStudentEnrollment(2);

            Assert.Equal(
                "DELETE FROM dbo.Enrollments WHERE EnrollmentID = @EnrollmentID;",
                req.GetSql());
        }

        [Fact]
        public void RemoveBulkEnrollments_GetParameters()
        {
            var req = new RemoveBulkStudentEnrollments(new[] { 1, 2, 3, 2, 1 });

            var p = req.GetParameters();
            var ids = (int[])p!.GetType().GetProperty("EnrollmentIDs")!.GetValue(p)!;

            Assert.Equal(5, ids.Length);
        }

        [Fact]
        public void RemoveEnrollment_GetParameters()
        {
            var req = new RemoveStudentEnrollment(2);

            var p = req.GetParameters();
            var id = (int)p!.GetType().GetProperty("EnrollmentID")!.GetValue(p)!;

            Assert.Equal(2, id);
        }


        [Fact]
        public void RemoveBulkEnrollments_GetSql()
        {
            var req = new RemoveBulkStudentEnrollments(new[] { 1, 2, 3, 2, 1 });

            Assert.Equal(
                "DELETE FROM dbo.Enrollments WHERE EnrollmentID IN @EnrollmentID;",
                req.GetSql());
        }

    }
}



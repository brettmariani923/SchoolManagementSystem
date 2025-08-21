using Teachers.Data.Requests.Students.Remove;

namespace Teachers.Test.DataRequestTests.Students
{
    public class RemoveTests
    {
        [Fact]
        public void RemoveStudent_GetSql_IsCorrect()
        {
            var req = new RemoveStudentByID(42);

            Assert.Equal(
                @"DELETE FROM dbo.Students" +
                  "WHERE StudentID = @StudentID;",
                req.GetSql());
        }

        [Fact]
        public void RemoveStudent_GetParameters_ProjectsId()
        {
            var req = new RemoveStudentByID(1337);
            var p = req.GetParameters()!;
            var id = (int)p.GetType().GetProperty("StudentID")!.GetValue(p)!;

            Assert.Equal(1337, id);
        }

        [Fact]
        public void RemoveBulkStudents_GetSql_IsCorrect()
        {
            var req = new RemoveBulkStudents(new[] { 1, 2, 3, 4 });

            Assert.Equal(
                @"DELETE FROM dbo.Students" +
                  "WHERE StudentID IN @StudentIDs;",
                req.GetSql());
        }

        [Fact]
        public void RemoveBulkStudents_GetParameters_CountMatches()
        {
            var ids = new[] { 1, 2, 3, 4 };               // no duplicates
            var req = new RemoveBulkStudents(ids);
            var p = req.GetParameters()!;
            var projected = (IEnumerable<int>)p.GetType().GetProperty("StudentIDs")!.GetValue(p)!;

            Assert.Equal(ids.Length, projected.Count()); // 3 == 3
        }

    }
}

using Teachers.Data.Requests.Courses.Remove;

namespace Teachers.Test.DataRequestTests.Courses
{
    public class RemoveTests
    {
        [Fact]
        public void RemoveCourse_GetSql_IsCorrect()
        {
            var req = new RemoveCourseByID(42);

            Assert.Equal(
                @"DELETE FROM dbo.Courses" +
                  "WHERE CourseID = @CourseID;",
                req.GetSql());
        }

        [Fact]
        public void RemoveCourse_GetParameters_ProjectsId()
        {
            var req = new RemoveCourseByID(1337);
            var p = req.GetParameters()!;
            var id = (int)p.GetType().GetProperty("CourseID")!.GetValue(p)!;

            Assert.Equal(1337, id);
        }

        [Fact]
        public void RemoveBulkCourses_GetSql_IsCorrect()
        {
            var req = new RemoveBulkCourses(new[] { 1, 2, 3, 4 });

            Assert.Equal(
                @"DELETE FROM dbo.Courses" +
                  "WHERE CourseID IN @CourseIDs;",
                req.GetSql());
        }

        [Fact]
        public void RemoveBulkCourses_GetParameters_CountMatches()
        {
            var ids = new[] { 1, 2, 3, 4 }; 
            var req = new RemoveBulkCourses(ids);
            var p = req.GetParameters()!;
            var projected = (IEnumerable<int>)p.GetType().GetProperty("CourseIDs")!.GetValue(p)!;

            Assert.Equal(ids.Length, projected.Count());
        }
    }
}

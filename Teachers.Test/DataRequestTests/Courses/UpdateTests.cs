using Teachers.Data.DTO;
using Teachers.Data.Requests.Courses.Update;

namespace Teachers.Test.DataRequestTests.Courses
{
    public class UpdateTests
    {
        private const string ExpectedSql =
            "UPDATE dbo.Courses " +
            "SET CourseID = @CourseID, " +
            "CourseName = @CourseName, " +
            "Credits  = @Credits, " +
            "SchoolID  = @SchoolID " +
            "WHERE CourseID = @CourseID;";

        [Fact]
        public void UpdateCourse_GetSql_IsCorrect()
        {
            var req = new UpdateCourse(courseID: 1, courseName: "Algebra I", credits: 3, schoolID: 42);

            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void UpdateCourse_GetParameters_ProjectsFields()
        {
            var req = new UpdateCourse(courseID: 7, courseName: "Biology", credits: 4, schoolID: 99);

            var p = req.GetParameters()!;
            var t = p.GetType();

            Assert.Equal(7, (int)t.GetProperty("CourseID")!.GetValue(p)!);
            Assert.Equal("Biology", (string)t.GetProperty("CourseName")!.GetValue(p)!);
            Assert.Equal(4, (int)t.GetProperty("Credits")!.GetValue(p)!);
            Assert.Equal(99, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
        }

        [Fact]
        public void UpdateBulkCourses_GetSql_IsCorrect()
        {
            var list = new[]
            {
                new Courses_Row { CourseID = 1, CourseName = "A", Credits = 1, SchoolID = 2 }
            };
            var req = new UpdateBulkCourses(list);

            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void UpdateBulkCourses_GetParameters_CountMatches()
        {
            var list = new[]
            {
                new Courses_Row { CourseID = 10, CourseName = "Chemistry", Credits = 4, SchoolID = 5 },
                new Courses_Row { CourseID = 11, CourseName = "Physics",   Credits = 4, SchoolID = 5 }
            };

            var req = new UpdateBulkCourses(list);
            var rows = ((IEnumerable<object>)req.GetParameters()!).ToList();

            Assert.Equal(list.Length, rows.Count);
        }
    }
}

using Teachers.Data.Rows;
using Teachers.Data.Requests.Courses.Update;

namespace Teachers.Test.DataRequestTests.Courses
{
    public class UpdateCourseTests
    {
        private const string ExpectedSql =
            @"UPDATE dbo.Courses
              SET CourseName = @CourseName,
                  Credits    = @Credits,
                  SchoolID   = @SchoolID
              WHERE CourseID = @CourseID;";

        [Fact]
        public void UpdateCourse_GetSql_IsCorrect()
        {
            var row = new Courses_Row
            {
                CourseID = 1,
                CourseName = "Algebra I",
                Credits = 3,
                SchoolID = 42
            };

            var req = new UpdateCourse(row);

            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void UpdateCourse_GetParameters_ProjectsFields()
        {
            var row = new Courses_Row
            {
                CourseID = 7,
                CourseName = "Biology",
                Credits = 4,
                SchoolID = 99
            };

            var req = new UpdateCourse(row);

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

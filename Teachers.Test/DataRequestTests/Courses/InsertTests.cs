using Teachers.Data.Rows;
using Teachers.Data.Requests.Courses.Insert;

namespace Teachers.Test.DataRequestTests.Courses
{
    public class InsertTests
    {
        private const string ExpectedSql =
            "INSERT INTO dbo.Courses (CourseName, Credits, SchoolID)" +
             "VALUES (@CourseName, @Credits, @SchoolID);";

        [Fact]
        public void InsertNewCourse_GetSql_IsExact()
        {
            var row = new Courses_Row
            {
                CourseName = "Algebra I",
                Credits = 3,
                SchoolID = 42
            };

            var req = new InsertNewCourse(row);

            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void InsertNewCourse_GetParameters_ProjectsFields()
        {
            var row = new Courses_Row
            {
                CourseName = "Algebra I",
                Credits = 3,
                SchoolID = 42
            };

            var req = new InsertNewCourse(row);

            var p = req.GetParameters()!;
            var t = p.GetType();

            Assert.Equal("Algebra I", (string)t.GetProperty("CourseName")!.GetValue(p)!);
            Assert.Equal(3, (int)t.GetProperty("Credits")!.GetValue(p)!);
            Assert.Equal(42, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
        }

        [Fact]
        public void InsertBulkNewCourses_GetSql_IsExact()
        {
            var rows = new[]
            {
                new Courses_Row { CourseName = "Algebra I", Credits = 3, SchoolID = 42 },
                new Courses_Row { CourseName = "Biology",   Credits = 4, SchoolID = 99 }
            };

            var req = new InsertBulkNewCourses(rows);

            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void InsertBulkNewCourses_GetParameters_ProjectsFirstItem()
        {
            var rows = new[]
            {
                new Courses_Row { CourseName = "Algebra I", Credits = 3, SchoolID = 42 },
                new Courses_Row { CourseName = "Biology",   Credits = 4, SchoolID = 99 }
            };

            var req = new InsertBulkNewCourses(rows);

            var list = ((IEnumerable<object>)req.GetParameters()!).ToList();
            Assert.Equal(rows.Length, list.Count);

            var first = list[0];
            var t = first.GetType();

            Assert.Equal("Algebra I", (string)t.GetProperty("CourseName")!.GetValue(first)!);
            Assert.Equal(3, (int)t.GetProperty("Credits")!.GetValue(first)!);
            Assert.Equal(42, (int)t.GetProperty("SchoolID")!.GetValue(first)!);
        }
    }
}

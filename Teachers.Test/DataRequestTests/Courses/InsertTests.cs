using Teachers.Data.DTO;
using Teachers.Data.Requests.Courses.Insert;

namespace Teachers.Test.DataRequestTests.Courses
{
    public class InsertTests
    {
        [Fact]
        public void InsertNewCourse_GetSql_IsExact()
        {
            var req = new InsertNewCourse("Algebra I", 3, 42);

            const string expected =
                "INSERT INTO dbo.Courses (CourseName, Credits, SchoolID)" +
                "VALUES (@CourseName, @Credits, @SchoolID);";

            Assert.Equal(expected, req.GetSql());
        }

        [Fact]
        public void InsertNewCourse_GetParameters_ProjectsFields()
        {
            var req = new InsertNewCourse("Algebra I", 3, 42);
            var p = req.GetParameters()!;
            var t = p.GetType();

            Assert.Equal("Algebra I", (string)t.GetProperty("CourseName")!.GetValue(p)!);
            Assert.Equal(3, (int)t.GetProperty("Credits")!.GetValue(p)!);
            Assert.Equal(42, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
        }

        [Fact]
        public void InsertBulkNewCourses_GetSql_IsExact()
        {
            var req = new InsertBulkNewCourses(new List<Courses_Row>());

            const string expected =
                "INSERT INTO dbo.Courses (CourseID, CourseName, Credits, SchoolID)" +
                "VALUES (@CourseID, @CourseName, @Credits, @SchoolID);";

            Assert.Equal(expected, req.GetSql());
        }

        [Fact]
        public void InsertBulkNewCourses_GetParameters_CountAndFirstRow()
        {
            var courses = new List<Courses_Row>
            {
                new() { CourseID = 10, CourseName = "Chemistry", Credits = 4, SchoolID = 5 },
                new() { CourseID = 11, CourseName = "Physics",   Credits = 4, SchoolID = 5 }
            };

            var req = new InsertBulkNewCourses(courses);
            var rows = ((IEnumerable<object>)req.GetParameters()!).ToList();

            Assert.Equal(2, rows.Count);

            var r0 = rows[0];
            var t0 = r0.GetType();

            Assert.Equal(10, (int)t0.GetProperty("CourseID")!.GetValue(r0)!);
            Assert.Equal("Chemistry", (string)t0.GetProperty("CourseName")!.GetValue(r0)!);
            Assert.Equal(4, (int)t0.GetProperty("Credits")!.GetValue(r0)!);
            Assert.Equal(5, (int)t0.GetProperty("SchoolID")!.GetValue(r0)!);
        }
    }
}



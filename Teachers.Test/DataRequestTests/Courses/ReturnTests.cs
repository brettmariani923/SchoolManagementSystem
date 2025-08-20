using Teachers.Data.Requests.Courses.Return;

namespace Teachers.Test.DataRequestTests.Courses
{
    public class ReturnTests
    {
        [Fact]
        public void GetSql_ShouldReturnExpectedSelectAllStatement()
        {
            var sut = new ReturnAllCourses();

            var sql = sut.GetSql();
            var expected =
                @"SELECT CourseID, CourseName, Credits, SchoolID" +
                  "FROM dbo.Courses;";

            static string Normalize(string s) => new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void GetParameters_ShouldBeNull()
        {
            var sut = new ReturnAllCourses();

            var parameters = sut.GetParameters();
            Assert.Null(parameters);
        }

        [Fact]
        public void GetSql_ShouldReturnExpectedSelectByIdStatement()
        {
            var sut = new ReturnCourseByID(1);

            var sql = sut.GetSql();
            var expected =
                @"SELECT CourseID, CourseName, Credits, SchoolID" +
                 "FROM dbo.Courses" +
                 "WHERE CourseID = @CourseID;";

            static string Normalize(string s) => new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void GetParameters_ShouldContainCourseID()
        {
            var sut = new ReturnCourseByID(7);

            var p = sut.GetParameters()!;
            var t = p.GetType();

            var idProp = t.GetProperty("CourseID");
            Assert.NotNull(idProp);

            var value = (int)idProp!.GetValue(p)!;
            Assert.Equal(7, value);
        }

        [Fact]
        public void GetParameters_HasOnlyExpectedPropertyName()
        {
            var sut = new ReturnCourseByID(1);

            var names = sut.GetParameters()!
                .GetType()
                .GetProperties()
                .Select(pi => pi.Name)
                .ToArray();

            Assert.Single(names);
            Assert.Equal("CourseID", names[0]);
        }
    }
}

using Teachers.Data.Requests.Teachers.Return;

namespace Teachers.Test.DataRequestTests.Teachers
{
    public class ReturnAllTeachers_UnitTests
    {
        [Fact]
        public void GetSql_ShouldReturnExpectedSelectAllStatement()
        {
            var sut = new ReturnAllTeachers();

            var sql = sut.GetSql();
            var expected =
                @"SELECT TeacherID, FirstName, LastName, SchoolID " +
                  "FROM dbo.Teachers;";

            static string Normalize(string s) => new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void GetParameters_ShouldBeNull()
        {
            var sut = new ReturnAllTeachers();

            var parameters = sut.GetParameters();
            Assert.Null(parameters);
        }

        [Fact]
        public void GetSql_ShouldReturnExpectedSelectByIdStatement()
        {
            var sut = new ReturnTeacherByID(1);

            var sql = sut.GetSql();
            var expected =
                @"SELECT TeacherID, FirstName, LastName, SchoolID" +
                 "FROM dbo.Teachers" +
                 "WHERE TeacherID = @TeacherID;"; 

            static string Normalize(string s) => new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void GetParameters_ShouldContainTeacherID()
        {
            var sut = new ReturnTeacherByID(7);

            var p = sut.GetParameters()!;
            var t = p.GetType();

            var idProp = t.GetProperty("TeacherID");
            Assert.NotNull(idProp);

            var value = (int)idProp!.GetValue(p)!;
            Assert.Equal(7, value);
        }

        [Fact]
        public void GetParameters_HasOnlyExpectedPropertyName()
        {
            var sut = new ReturnTeacherByID(1);

            var names = sut.GetParameters()!
                .GetType()
                .GetProperties()
                .Select(pi => pi.Name)
                .ToArray();

            Assert.Single(names);
            Assert.Equal("TeacherID", names[0]);
        }

        [Fact]
        public void Constructor_ShouldSetTeacherIDProperty()
        {
            var sut = new ReturnTeacherByID(42);
            var parameters = sut.GetParameters()!;
            var teacherId = parameters.GetType().GetProperty("TeacherID")!.GetValue(parameters);
            Assert.Equal(42, teacherId);
        }

        [Fact]
        public void GetSql_ShouldContainParameterPlaceholder()
        {
            var sut = new ReturnTeacherByID(1);
            var sql = sut.GetSql();
            Assert.Contains("@TeacherID", sql);
        }

        [Fact]
        public void MultipleInstances_WithSameId_ShouldBeEqual()
        {
            var a = new ReturnTeacherByID(5);
            var b = new ReturnTeacherByID(5);
            Assert.Equal(a.GetSql(), b.GetSql());
            Assert.Equal(
                a.GetParameters()!.GetType().GetProperty("TeacherID")!.GetValue(a.GetParameters()),
                b.GetParameters()!.GetType().GetProperty("TeacherID")!.GetValue(b.GetParameters())
            );
        }

        [Fact]
        public void GetSql_ShouldNotContainSemicolonInMiddle()
        {
            var sut = new ReturnTeacherByID(1);
            var sql = sut.GetSql();
            var index = sql.IndexOf(';');
            Assert.True(index == sql.Length - 1, "Semicolon should only be at the end of the SQL statement.");
        }
    }
}

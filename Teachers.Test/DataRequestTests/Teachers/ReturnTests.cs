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
                @"SELECT TeacherID, FirstName, LastName, SchoolID
                  FROM dbo.Teachers;";

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
            var sut = new ReturnTeacherByID(42);

            var sql = sut.GetSql();
            var expected =
                @"SELECT TeacherID, FirstName, LastName, SchoolID
                  FROM dbo.Teachers
                  WHERE TeacherID = @TeacherID;";

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
    }
}

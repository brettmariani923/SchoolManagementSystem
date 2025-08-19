using Teachers.Data.DTO;
using Teachers.Data.Requests.Teachers.Update; 

namespace Teachers.Test.DataRequestTests.Teachers
{
    public class UpdateBulkTeachers_UnitTests
    {
        [Fact]
        public void Ctor_GivenNullTeachers_Throws()
        {
            IEnumerable<Teachers_DTO>? teachers = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new UpdateBulkTeachers(teachers!));
            Assert.Equal("teachers", ex.ParamName);
        }

        [Fact]
        public void GetSql_ShouldReturnExpectedUpdateStatement_Bulk()
        {
            var sut = new UpdateBulkTeachers(new List<Teachers_DTO>());

            var sql = sut.GetSql();
            var expected =
                @"UPDATE Teachers
                  SET FirstName = @FirstName,
                      LastName  = @LastName,
                      SchoolID  = @SchoolID
                  WHERE TeacherID = @TeacherID;";

            static string Normalize(string s) => new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void GetParameters_ShouldProject_AllRequiredFields_PerTeacher()
        {
            var teachers = new List<Teachers_DTO>
            {
                new() { TeacherID = 1, FirstName = "John", LastName = "Doe",     SchoolID = 10 },
                new() { TeacherID = 2, FirstName = "Jane", LastName = "Smith",   SchoolID = 10 },
                new() { TeacherID = 3, FirstName = "Ada",  LastName = "Lovelace",SchoolID = 20 }
            };

            var sut = new UpdateBulkTeachers(teachers);

            var enumerable = sut.GetParameters() as IEnumerable<object>;
            Assert.NotNull(enumerable);

            var parameters = enumerable!.ToArray();
            Assert.Equal(teachers.Count, parameters.Length);

            for (int i = 0; i < teachers.Count; i++)
            {
                var p = parameters[i];
                var t = teachers[i];

                AssertPropertyEquals(p, "TeacherID", t.TeacherID);
                AssertPropertyEquals(p, "FirstName", t.FirstName);
                AssertPropertyEquals(p, "LastName", t.LastName);
                AssertPropertyEquals(p, "SchoolID", t.SchoolID);
            }

            static void AssertPropertyEquals(object obj, string name, object expected)
            {
                var pi = obj.GetType().GetProperty(name);
                Assert.NotNull(pi);
                var value = pi!.GetValue(obj);
                Assert.Equal(expected, value);
            }
        }

        [Fact]
        public void GetParameters_GivenEmptyList_ReturnsEmptySequence()
        {
            var sut = new UpdateBulkTeachers(new List<Teachers_DTO>());

            var enumerable = sut.GetParameters() as IEnumerable<object>;
            Assert.NotNull(enumerable);
            Assert.Empty(enumerable!);
        }

        [Fact]
        public void GetSql_ShouldReturnExpectedUpdateStatement()
        {
            var sut = new UpdateTeacher(1, "John", "Doe", 10);

            var sql = sut.GetSql();
            var expected =
                @"UPDATE dbo.Teachers
                  SET TeacherID = @TeacherID,
                      FirstName = @FirstName,
                      LastName  = @LastName,
                      SchoolID  = @SchoolID
                  WHERE TeacherID = @TeacherID;";

            static string Normalize(string s) => new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void GetParameters_ShouldProject_AllFields()
        {
            var sut = new UpdateTeacher(42, "Ada", "Lovelace", 7);

            var p = sut.GetParameters()!;
            var t = p.GetType();

            Assert.Equal(42, (int)t.GetProperty("TeacherID")!.GetValue(p)!);
            Assert.Equal("Ada", t.GetProperty("FirstName")!.GetValue(p));
            Assert.Equal("Lovelace", t.GetProperty("LastName")!.GetValue(p));
            Assert.Equal(7, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
        }

        [Fact]
        public void GetParameters_ProducesAnonymousObjectWithExpectedPropertyNames()
        {
            var sut = new UpdateTeacher(1, "J", "D", 1);

            var names = sut.GetParameters()!
                .GetType()
                .GetProperties()
                .Select(pi => pi.Name)
                .OrderBy(n => n)
                .ToArray();

            Assert.Equal(new[] { "FirstName", "LastName", "SchoolID", "TeacherID" }, names);
        }
    }
}

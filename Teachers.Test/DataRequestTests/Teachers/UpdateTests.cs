using Teachers.Data.Rows;
using Teachers.Data.Requests.Teachers.Update;

namespace Teachers.Test.DataRequestTests.Teachers
{
    public class UpdateBulkTeachers_UnitTests
    {
        [Fact]
        public void Ctor_GivenNullTeachers_Throws()
        {
            IEnumerable<Teachers_Row>? teachers = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new UpdateBulkTeachers(teachers!));
            Assert.Equal("teachers", ex.ParamName);
        }

        [Fact]
        public void GetParameters_ShouldProject_AllRequiredFields_PerTeacher()
        {
            var teachers = new List<Teachers_Row>
            {
                new() { TeacherID = 1, FirstName = "John", LastName = "Doe",      SchoolID = 10 },
                new() { TeacherID = 2, FirstName = "Jane", LastName = "Smith",    SchoolID = 10 },
                new() { TeacherID = 3, FirstName = "Ada",  LastName = "Lovelace", SchoolID = 20 }
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
        public void GetSql_ShouldReturnExpectedUpdateStatement()
        {
            var row = new Teachers_Row { TeacherID = 1, FirstName = "John", LastName = "Doe", SchoolID = 10 };
            var sut = new UpdateTeacher(row);

            var sql = sut.GetSql();
            var expected =
                @"UPDATE dbo.Teachers
                  SET FirstName = @FirstName,
                      LastName  = @LastName,
                      SchoolID  = @SchoolID
                  WHERE TeacherID = @TeacherID;";

            static string Normalize(string s) => new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void GetParameters_ShouldProject_AllFields()
        {
            var row = new Teachers_Row { TeacherID = 42, FirstName = "Ada", LastName = "Lovelace", SchoolID = 7 };
            var sut = new UpdateTeacher(row);

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
            var row = new Teachers_Row { TeacherID = 1, FirstName = "J", LastName = "D", SchoolID = 1 };
            var sut = new UpdateTeacher(row);

            var names = sut.GetParameters()!
                .GetType()
                .GetProperties()
                .Select(pi => pi.Name)
                .OrderBy(n => n)
                .ToArray();

            Assert.Equal(new[] { "FirstName", "LastName", "SchoolID", "TeacherID" }, names);
        }

        [Fact]
        public void GetSql_ShouldReturnExpectedUpdateBulkStatement()
        {
            var teachers = new[]
            {
            new Teachers_Row { TeacherID = 1, FirstName = "A", LastName = "B", SchoolID = 100 },
            new Teachers_Row { TeacherID = 2, FirstName = "C", LastName = "D", SchoolID = 101 }
        };

            var sut = new UpdateBulkTeachers(teachers);

            var sql = sut.GetSql();
            Assert.Contains("UPDATE dbo.Teachers", sql);
            Assert.Contains("@TeacherID", sql);
            Assert.Contains("@FirstName", sql);
            Assert.Contains("@LastName", sql);
            Assert.Contains("@SchoolID", sql);
        }
    }
}
      

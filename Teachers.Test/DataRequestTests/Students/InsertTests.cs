using Teachers.Data.Rows;
using Teachers.Data.Requests.Students.Insert;

namespace Teachers.Test.Requests.Students
{
    public class InsertBulkStudentsTests
    {
        [Fact]
        public void GetSql_ShouldMatchExpectedInsertStatement()
        {
            // Arrange
            var students = new[]
            {
                new Students_Row { FirstName = "Naruto", LastName = "Uzumaki", Year = 1 },
                new Students_Row { FirstName = "Sauske", LastName = "Uchiha",  Year = 2 },
            };
            var request = new InsertBulkStudents(students);

            // Act
            var sql = request.GetSql();

            // Assert 
            const string expected =
              @"INSERT INTO dbo.Students (FirstName, LastName, [Year], SchoolID) " +
              "VALUES (@FirstName, @LastName, @Year, @SchoolID);";
            Assert.Equal(expected, sql);
        }

        [Fact]
        public void GetParameters_ShouldYieldOneParamObjectPerStudent_AndUseCtorSchoolID()
        {   //Arrange
            var students = new List<Students_Row>
            {
                new() { FirstName = "Shikamaru", LastName = "Nara",    Year = 2 },
                new() { FirstName = "Choji",     LastName = "Akimichi", Year = 2 },
                new() { FirstName = "Ino",       LastName = "Yamanaka", Year = 2 }
            };

            var request = new InsertBulkStudents(students);

            // Act
            var obj = request.GetParameters();
            Assert.NotNull(obj);

            var list = ((IEnumerable<object>)obj!).ToList();
            Assert.Equal(3, list.Count);

            // Assert
            var p0 = list[0]; var t0 = p0.GetType();
            Assert.Equal("Shikamaru", t0.GetProperty("FirstName")!.GetValue(p0));
            Assert.Equal("Nara", t0.GetProperty("LastName")!.GetValue(p0));
            Assert.Equal(2, t0.GetProperty("Year")!.GetValue(p0));

            var p1 = list[1]; var t1 = p1.GetType();
            Assert.Equal("Choji", t1.GetProperty("FirstName")!.GetValue(p1));
            Assert.Equal("Akimichi", t1.GetProperty("LastName")!.GetValue(p1));
            Assert.Equal(2, t1.GetProperty("Year")!.GetValue(p1));

            var p2 = list[2]; var t2 = p2.GetType();
            Assert.Equal("Ino", t2.GetProperty("FirstName")!.GetValue(p2));
            Assert.Equal("Yamanaka", t2.GetProperty("LastName")!.GetValue(p2));
            Assert.Equal(2, t2.GetProperty("Year")!.GetValue(p2));
        }

        [Fact]
        public void Ctor_NullStudents_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new InsertBulkStudents(null!));
        }

        [Fact]
        public void Ctor_EmptyStudents_Throws()
        {
            Assert.Throws<ArgumentException>(() => new InsertBulkStudents(Array.Empty<Students_Row>()));
        }

        [Fact]
        public void Ctor_WithNullRow_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new InsertNewStudent(null!));
        }

        [Theory]
        [InlineData(null, "Doe", 2, 1, "FirstName")]
        [InlineData("   ", "Doe", 2, 1, "FirstName")]
        [InlineData("John", null, 2, 1, "LastName")]
        [InlineData("John", "   ", 2, 1, "LastName")]
        public void Ctor_WithMissingNames_Throws(string first, string last, int year, int schoolId, string paramName)
        {
            var row = new Students_Row { FirstName = first, LastName = last, Year = year, SchoolID = schoolId };
            var ex = Assert.Throws<ArgumentException>(() => new InsertNewStudent(row));
            Assert.Equal(paramName, ex.ParamName);
        }

        [Theory]
        [InlineData(0, 1, "row.Year")]
        [InlineData(-1, 1, "row.Year")]
        [InlineData(2, 0, "row.SchoolID")]
        [InlineData(2, -5, "row.SchoolID")]
        public void Ctor_WithNonPositiveYearOrSchool_Throws(int year, int schoolId, string paramName)
        {
            var row = new Students_Row { FirstName = "John", LastName = "Doe", Year = year, SchoolID = schoolId };
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new InsertNewStudent(row));
            Assert.Equal(paramName, ex.ParamName);
        }

        [Fact]
        public void GetSql_IsExact()
        {
            var row = new Students_Row { FirstName = "John", LastName = "Doe", Year = 2, SchoolID = 3 };
            var req = new InsertNewStudent(row);

            const string expected =
                @"INSERT INTO dbo.Students (FirstName, LastName, [Year], SchoolID) " +
                "VALUES (@FirstName, @LastName, @Year, @SchoolID);";

            Assert.Equal(expected, req.GetSql());
        }

        [Fact]
        public void GetParameters_MapsAndTrimsValues()
        {
            var row = new Students_Row
            {
                FirstName = "  John  ",
                LastName = "  Doe ",
                Year = 2,
                SchoolID = 3
            };

            var req = new InsertNewStudent(row);
            var p = req.GetParameters();

            var t = p.GetType();
            Assert.Equal("John", (string)t.GetProperty("FirstName")!.GetValue(p)!);
            Assert.Equal("Doe", (string)t.GetProperty("LastName")!.GetValue(p)!);
            Assert.Equal(2, (int)t.GetProperty("Year")!.GetValue(p)!);
            Assert.Equal(3, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
        }
    }
}


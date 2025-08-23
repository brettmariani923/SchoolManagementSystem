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
                new Students_Row { FirstName = "Naruto", LastName = "Uzumaki", Year = 1, SchoolID = 1 },
                new Students_Row { FirstName = "Sauske", LastName = "Uchiha",  Year = 2, SchoolID = 2 },
            };
            var request = new InsertBulkStudents(students, schoolID: 7);

            // Act
            var sql = request.GetSql();

            // Assert 
            const string expected =
              @"INSERT INTO dbo.Students (FirstName, LastName, [Year], SchoolID)" +
              "VALUES (@FirstName, @LastName, @Year, @SchoolID);";
            Assert.Equal(expected, sql);
        }

        // This test checks two things:
        // 1. For each input student, GetParameters() should return one parameter object.
        //    (3 students in → 3 parameter objects out.)
        // 2. The FirstName, LastName, and Year come from the DTOs,
        //    but the SchoolID must always come from the constructor argument,
        //    ignoring whatever value was in the DTO.
        [Fact]
        public void GetParameters_ShouldYieldOneParamObjectPerStudent_AndUseCtorSchoolID()
        {   //Arrange
            var students = new List<Students_Row>
            {
                new() { FirstName = "Shikamaru", LastName = "Nara",    Year = 2 },
                new() { FirstName = "Choji",     LastName = "Akimichi", Year = 2 },
                new() { FirstName = "Ino",       LastName = "Yamanaka", Year = 2 }
            };
            const int enforcedSchoolId = 12;

            var request = new InsertBulkStudents(students, enforcedSchoolId);

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
            Assert.Throws<ArgumentNullException>(() => new InsertBulkStudents(null!, 99));
        }

        [Fact]
        public void Ctor_EmptyStudents_Throws()
        {
            Assert.Throws<ArgumentException>(() => new InsertBulkStudents(Array.Empty<Students_Row>(), 99));
        }
    }
}


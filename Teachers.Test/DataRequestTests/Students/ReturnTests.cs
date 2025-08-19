using Teachers.Data.Requests.Students.Return;

namespace Teachers.Test.DataRequestTests.Students
{
    public class ReturnTests
    {
        [Fact]
        public void ReturnAllStudents_GetSql_IsExactAndSchemaQualified()
        {
            // Arrange
            var req = new ReturnAllStudents();

            // Act
            var sql = req.GetSql();

            // Assert
            const string expected =
                @"SELECT StudentID, FirstName, LastName, [Year], SchoolID
                  FROM dbo.Students;";

            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void ReturnAllStudents_GetParameters_IsNullOrAnonymousNull()
        {
            // Arrange
            var req = new ReturnAllStudents();

            // Act
            var parameters = req.GetParameters();

            // Assert
            Assert.True(parameters is null, "Expected null parameters for ReturnAllStudents.");
        }

        [Fact]
        public void ReturnStudentById_GetSql_IsExactAndSchemaQualified()
        {
            // Arrange
            var req = new ReturnStudentById(123);

            // Act
            var sql = req.GetSql();

            // Assert
            const string expected =
                @"SELECT StudentID, FirstName, LastName, [Year], SchoolID
                  FROM dbo.Students
                  WHERE StudentID = @StudentID;";

            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void ReturnStudentById_GetParameters_ContainsStudentID()
        {
            // Arrange
            var id = 987;
            var req = new ReturnStudentById(id);

            // Act
            var p = req.GetParameters();

            // Assert
            Assert.NotNull(p);
            var prop = p!.GetType().GetProperty("StudentID");
            Assert.NotNull(prop);
            var value = (int)prop!.GetValue(p)!;
            Assert.Equal(id, value);
        }

        [Fact]
        public void ReturnStudentById_InvalidId_Throws()
        {
            // Arrange
     
            // Act + Assert
            Assert.ThrowsAny<ArgumentException>(() => new ReturnStudentById(0));
            Assert.ThrowsAny<ArgumentException>(() => new ReturnStudentById(-1));
        }

        //Helper
        private static string Normalize(string s) =>
            new string(s.AsSpan().ToString().ToCharArray())
                .Replace(" ", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty)
                .ToLowerInvariant();
    }
}

using Teachers.Data.DTO;
using Teachers.Data.Requests.Students;

namespace Teachers.Test.DataRequestTests.Students
{
    public class UpdateTests
    {
        private const string ExpectedSql =
        "UPDATE dbo.Students " +
        "SET StudentID = @StudentID, " +
        "TeacherID = @TeacherID, " +
        "CourseID  = @CourseID, " +
        "SchoolID  = @SchoolID " +
        "WHERE StudentID = @StudentID;";


        [Fact]
        public void UpdateStudent_GetSql_IsCorrect()
        {
            var dto = new Students_Row { StudentID = 1, FirstName = "Ash", LastName = "Ketchum", Year = 3, SchoolID = 42 };
            var req = new UpdateStudent(dto);

            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void UpdateStudent_GetParameters_ProjectsFields()
        {
            var dto = new Students_Row { StudentID = 7, FirstName = "Misty", LastName = "Waterflower", Year = 2, SchoolID = 99 };
            var req = new UpdateStudent(dto);

            var p = req.GetParameters()!;
            var t = p.GetType();

            Assert.Equal(7, (int)t.GetProperty("StudentID")!.GetValue(p)!);
            Assert.Equal("Misty", (string)t.GetProperty("FirstName")!.GetValue(p)!);
            Assert.Equal("Waterflower", (string)t.GetProperty("LastName")!.GetValue(p)!);
            Assert.Equal(2, (int)t.GetProperty("Year")!.GetValue(p)!);
            Assert.Equal(99, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
        }

        [Fact]
        public void UpdateBulkStudents_GetSql_IsCorrect()
        {
            var list = new[] { new Students_Row { StudentID = 1, FirstName = "A", LastName = "B", Year = 1, SchoolID = 2 } };
            var req = new UpdateBulkStudents(list);

            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void UpdateBulkStudents_GetParameters_CountMatches()
        {
            var list = new[]
            {
                new Students_Row { StudentID = 10, FirstName = "Brock",  LastName = "Harrison", Year = 4, SchoolID = 5 },
                new Students_Row { StudentID = 11, FirstName = "Tracey", LastName = "Sketchit", Year = 1, SchoolID = 5 }
            };

            var req = new UpdateBulkStudents(list);
            var rows = ((IEnumerable<object>)req.GetParameters()!).ToList();

            Assert.Equal(list.Length, rows.Count);
        }
    }
}

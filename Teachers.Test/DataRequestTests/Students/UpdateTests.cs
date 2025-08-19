using System;
using System.Collections.Generic;
using System.Linq;
using Teachers.Data.DTO;
using Teachers.Data.Requests.Students;
using Teachers.Domain.Interfaces;
using Xunit;

namespace Teachers.Test.DataRequestTests.Students
{
    public class UpdateTests
    {
        private const string ExpectedSql =
            @"UPDATE Students
              SET FirstName = @FirstName,
                  LastName  = @LastName,
                  [Year]    = @Year,
                  SchoolID  = @SchoolID
              WHERE StudentID = @StudentID;";

        [Fact]
        public void UpdateStudent_Implements_IDataExecute()
        {
            var dto = MakeDto(1, "Ash", "Ketchum", 3, 42);
            var req = new UpdateStudent(dto); 
            Assert.IsAssignableFrom<IDataExecute>(req);
        }

        [Fact]
        public void UpdateStudent_GetSql_Returns_Expected()
        {
            var dto = MakeDto(1, "Ash", "Ketchum", 3, 42);
            var req = new UpdateStudent(dto);

            var sql = req.GetSql().NormalizeWs();
            Assert.Equal(ExpectedSql.NormalizeWs(), sql);
        }

        [Fact]
        public void UpdateStudent_GetParameters_Maps_All_Fields()
        {
            var dto = MakeDto(7, "Misty", "Waterflower", 2, 99);
            var req = new UpdateStudent(dto);

            var p = req.GetParameters();
            Assert.NotNull(p);

            var anon = ToDict(p);
            Assert.Equal(7, anon["StudentID"]);
            Assert.Equal("Misty", anon["FirstName"]);
            Assert.Equal("Waterflower", anon["LastName"]);
            Assert.Equal(2, anon["Year"]);
            Assert.Equal(99, anon["SchoolID"]);
        }

        [Fact]
        public void UpdateBulkStudents_Implements_IDataExecute()
        {
            var list = new[] { MakeDto(1, "A", "B", 1, 2) };
            var req = new UpdateBulkStudents(list);
            Assert.IsAssignableFrom<IDataExecute>(req);
        }

        [Fact]
        public void UpdateBulkStudents_GetSql_Returns_Expected()
        {
            var list = new[] { MakeDto(1, "A", "B", 1, 2) };
            var req = new UpdateBulkStudents(list);

            var sql = req.GetSql().NormalizeWs();
            Assert.Equal(ExpectedSql.NormalizeWs(), sql);
        }

        [Fact]
        public void UpdateBulkStudents_GetParameters_Produces_One_Row_Per_DTO()
        {
            var list = new[]
            {
                MakeDto(10, "Brock", "Harrison", 4, 5),
                MakeDto(11, "Tracey", "Sketchit", 1, 5)
            };

            var req = new UpdateBulkStudents(list);
            var rows = (req.GetParameters() as IEnumerable<object>)?.ToList();

            Assert.NotNull(rows);
            Assert.Equal(2, rows!.Count);

            var r1 = ToDict(rows[0]);
            Assert.Equal(10, r1["StudentID"]);
            Assert.Equal("Brock", r1["FirstName"]);
            Assert.Equal("Harrison", r1["LastName"]);
            Assert.Equal(4, r1["Year"]);
            Assert.Equal(5, r1["SchoolID"]);

            var r2 = ToDict(rows[1]);
            Assert.Equal(11, r2["StudentID"]);
            Assert.Equal("Tracey", r2["FirstName"]);
            Assert.Equal("Sketchit", r2["LastName"]);
            Assert.Equal(1, r2["Year"]);
            Assert.Equal(5, r2["SchoolID"]);
        }

        //helpers

        private static Students_DTO MakeDto(int id, string first, string last, int year, int schoolId) =>
            new Students_DTO
            {
                StudentID = id,
                FirstName = first,
                LastName = last,
                Year = year,
                SchoolID = schoolId
            };

        private static Dictionary<string, object?> ToDict(object anon) =>
            anon.GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(anon));

    }

    internal static class StringExtensions
    {
        public static string NormalizeWs(this string s) =>
            new string(s.Where(c => !char.IsWhiteSpace(c) || c == ' ').ToArray())
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("  ", " ")
                .Trim();
    }
}



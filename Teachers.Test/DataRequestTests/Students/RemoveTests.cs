using Teachers.Data.Requests.Students.Remove;

namespace Teachers.Test.DataRequestTests.Students
{
    public class RemoveTests
    {
        [Fact]
        public void RemoveStudent_GetSql_IsExactAndSchemaQualified()
        {
            // Arrange
            var req = new RemoveStudent(42);

            // Act
            var sql = req.GetSql();

            // Assert
            const string expected =
                @"DELETE FROM dbo.Students
                  WHERE StudentID = @StudentID;";

            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void RemoveStudent_GetParameters_ContainsStudentID()
        {
            // Arrange
            var id = 1337;
            var req = new RemoveStudent(id);

            // Act
            var parameters = req.GetParameters();

            // Assert
            Assert.NotNull(parameters);

            var studentIdProp = parameters!.GetType().GetProperty("StudentID");
            Assert.NotNull(studentIdProp);
            var value = (int)studentIdProp!.GetValue(parameters)!;
            Assert.Equal(id, value);
        }

        [Fact]
        public void RemoveBulkStudents_GetSql_IsExactAndSchemaQualified()
        {
            // Arrange
            var req = new RemoveBulkStudents(new[] { 1, 2, 3 });

            // Act
            var sql = req.GetSql();

            // Assert
            const string expected =
                @"DELETE FROM dbo.Students
                  WHERE StudentID = @StudentID;";

            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void RemoveBulkStudents_GetParameters_ProjectsAllIds()
        {
            // Arrange
            var ids = new[] { 5, 9, 12 };
            var req = new RemoveBulkStudents(ids);

            // Act
            var parameters = req.GetParameters();

            // Assert
            Assert.NotNull(parameters);

            var list = ((IEnumerable<object>)parameters!).ToList();
            Assert.Equal(ids.Length, list.Count);

            var projectedIds = list
                .Select(p => (int)p.GetType().GetProperty("StudentID")!.GetValue(p)!)
                .ToArray();

            Assert.True(ids.SequenceEqual(projectedIds),
                $"Projected IDs mismatch. Expected: {string.Join(",", ids)}  Actual: {string.Join(",", projectedIds)}");
        }

        [Fact]
        public void RemoveBulkStudents_NullIds_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int>? ids = null;

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => new RemoveBulkStudents(ids!));
        }

        [Fact]
        public void RemoveBulkStudents_EmptyIds_ThrowsArgumentException()
        {
            // Arrange
            var ids = Array.Empty<int>();

            // Act + Assert
            Assert.Throws<ArgumentException>(() => new RemoveBulkStudents(ids));
        }

        // Helpers
        private static string Normalize(string s) =>
            new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLowerInvariant();
    }
}

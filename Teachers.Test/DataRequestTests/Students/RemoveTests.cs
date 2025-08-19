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

        [Fact]
        public void RemoveBulkStudents_DuplicateIds_ThrowsArgumentException()
        {
            // Arrange
            var ids = new[] { 1, 2, 2, 3 };
            // Act + Assert
            Assert.Throws<ArgumentException>(() => new RemoveBulkStudents(ids));
        }

        [Fact]
        public void RemoveBulkStudents_NegativeOrZeroId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var ids = new[] { 1, -2, 3 };
            // Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new RemoveBulkStudents(ids));
        }

        [Fact]
        public void RemoveBulkStudents_GetParameters_HandlesLargeIdCollections()
        {
            // Arrange
            var ids = Enumerable.Range(1, 1000).ToArray();
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
                $"Projected IDs mismatch. Expected: {string.Join(",", ids.Take(10))}...  Actual: {string.Join(",", projectedIds.Take(10))}...");
        }

        [Fact]
        public void RemoveBulkStudents_GetParameters_HandlesSingleId()
        {
            // Arrange
            var ids = new[] { 42 };
            var req = new RemoveBulkStudents(ids);
            // Act
            var parameters = req.GetParameters();
            // Assert
            Assert.NotNull(parameters);
            var list = ((IEnumerable<object>)parameters!).ToList();
            Assert.Single(list);
            var projectedId = (int)list[0].GetType().GetProperty("StudentID")!.GetValue(list[0])!;
            Assert.Equal(42, projectedId);
        }
        // Helpers
        private static string Normalize(string s) =>
            new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLowerInvariant();
    }
}

using Teachers.Data.Requests.Teachers.Remove;

namespace Teachers.Test.DataRequestTests.Teachers
{
    public class RemoveBulkTeachers_UnitTests
    {
        [Fact]
        public void Ctor_NullIds_Throws()
        {
            IEnumerable<int>? ids = null;
            Assert.Throws<ArgumentNullException>(() => new RemoveBulkTeachers(ids!));
        }

        [Fact]
        public void GetSql_IsDeleteForTeacherIds()
        {
            var req = new RemoveBulkTeachers(new[] { 1, 2, 3 });
            var sql = req.GetSql();

            Assert.Contains("DELETE FROM", sql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("TeacherID", sql);
            Assert.Contains("IN @TeacherIDs", sql);
        }

        [Fact]
        public void GetParameters_CountMatchesInput()
        {
            var ids = new[] { 10, 20, 30 };
            var req = new RemoveBulkTeachers(ids);

            var p = req.GetParameters()!;
            var teacherIds = (IEnumerable<int>)p.GetType().GetProperty("TeacherIDs")!.GetValue(p)!;

            Assert.Equal(ids.Length, teacherIds.Count());
        }
    }
}

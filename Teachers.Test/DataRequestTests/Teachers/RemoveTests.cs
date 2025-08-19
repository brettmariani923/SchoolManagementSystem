using Teachers.Data.Requests.Teachers.Remove;


namespace Teachers.Test.DataRequestTests.Teachers
{
    public class RemoveBulkTeachers_UnitTests
    {
        [Fact]
        public void Ctor_GivenNullIds_Throws()
        {
            IEnumerable<int>? ids = null;
            var ex = Assert.Throws<ArgumentNullException>(() => new RemoveBulkTeachers(ids!));
            Assert.Equal("teacherIds", ex.ParamName);
        }

        [Fact]
        public void GetSql_ShouldReturnExpectedDeleteStatement()
        {
            var sut = new RemoveBulkTeachers(Array.Empty<int>());

            var sql = sut.GetSql();
            var expected = "DELETE FROM Teachers WHERE TeacherID = @TeacherID;";

            string Normalize(string s) => new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.Equal(Normalize(expected), Normalize(sql));
        }

        [Fact]
        public void GetParameters_ShouldYieldOneParameterObjectPerId()
        {
            var ids = new[] { 10, 20, 30 };
            var sut = new RemoveBulkTeachers(ids);

            var enumerable = sut.GetParameters() as IEnumerable<object>;
            Assert.NotNull(enumerable);

            var parameters = enumerable!.ToArray();
            Assert.Equal(ids.Length, parameters.Length);

            for (int i = 0; i < ids.Length; i++)
            {
                var p = parameters[i];
                var prop = p.GetType().GetProperty("TeacherID");
                Assert.NotNull(prop);
                var value = (int)prop!.GetValue(p)!;
                Assert.Equal(ids[i], value);
            }
        }

    }
}

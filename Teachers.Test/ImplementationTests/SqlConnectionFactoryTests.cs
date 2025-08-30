using System.Data;
using Microsoft.Data.SqlClient;
using Teachers.Data.Implementation;
using Teachers.Test.Helpers;

namespace Teachers.Test.ImplementationTests
{
    public class SqlConnectionFactoryTests
    {

        [Theory]
        [MemberData(nameof(Hidden.ConnectionStrings), MemberType = typeof(Hidden))]
        public void NewConnection_WithValidConnectionString_ReturnsClosedSqlConnection(string connectionString)
        {
            var factory = new SqlConnectionFactory(connectionString);
            using var conn = factory.NewConnection();

            Assert.NotNull(conn);
            Assert.IsAssignableFrom<IDbConnection>(conn);
            Assert.Equal(ConnectionState.Closed, conn.State);
            Assert.IsType<SqlConnection>(conn);
            Assert.Equal(connectionString, conn.ConnectionString);
        }

    [Fact]
        public void Ctor_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SqlConnectionFactory(null!));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Ctor_WithEmptyOrWhitespace_ThrowsArgumentException(string bad)
        {
            Assert.Throws<ArgumentException>(() => new SqlConnectionFactory(bad));
        }
    }
}

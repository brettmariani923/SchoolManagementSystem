using System;
using System.Data;
using Microsoft.Data.SqlClient; // or System.Data.SqlClient if that’s what you use
using Xunit;

namespace Teachers.Test.ImplementationTests
{
    public class SqlConnectionFactoryTests
    {
        [Theory]
        [Helpers(nameof(Hidden.ConnectionStrings), Teacher = typeof(Hidden))]
        public void NewConnection_WithValidConnectionString_ReturnsClosedSqlConnection(string connectionString)
        {
            // Arrange
            var factory = new SqlConnectionFactory(connectionString);

            // Act
            using var conn = factory.NewConnection();

            // Assert
            Assert.NotNull(conn);
            Assert.IsType<SqlConnection>(conn);
            Assert.Equal(connectionString, conn.ConnectionString);
            Assert.Equal(ConnectionState.Closed, conn.State); 
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Ctor_WithNullOrEmpty_ThrowsArgumentException(string bad)
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentException>(() => new SqlConnectionFactory(bad));
        }

    }
}

using System.Data;
using Microsoft.Data.Sqlite;
using Teachers.Domain.Interfaces;

namespace Teachers.Test.Helpers
{
    public sealed class SqliteConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqliteConnectionFactory(string? connectionString = null)
        {
            _connectionString = connectionString ?? "Data Source=:memory:";
        }

        public IDbConnection NewConnection()
        {
            var conn = new SqliteConnection(_connectionString);
            return conn;
        }
    }
}

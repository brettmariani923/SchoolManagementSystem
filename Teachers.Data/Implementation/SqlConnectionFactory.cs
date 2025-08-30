using Microsoft.Data.SqlClient;
using System.Data;
using Teachers.Data.Interfaces;

namespace Teachers.Data.Implementation
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        #region Private

        private readonly string _connectionString;

        #endregion

        #region Constructor

        public SqlConnectionFactory(string connectionString)
        {
            if (connectionString is null)
                throw new ArgumentNullException(nameof(connectionString));

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be empty or whitespace.", nameof(connectionString));

            _connectionString = connectionString;
        }

        #endregion

        #region Public IDbCOnnectionFactory Method

        public IDbConnection NewConnection() => new SqlConnection(_connectionString);

        #endregion
    }
}

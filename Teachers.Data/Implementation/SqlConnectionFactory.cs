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

        public SqlConnectionFactory(string connectionString) => _connectionString = connectionString;

        #endregion

        #region Public IDbCOnnectionFactory Method

        public IDbConnection NewConnection() => new SqlConnection(_connectionString);

        #endregion
    }
}

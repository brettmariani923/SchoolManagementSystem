using Teachers.Data.Interfaces;
using System.Data;
using Dapper;

namespace Teachers.Data.Implementation
{
    public class DataAccess : IDataAccess
    {
        #region Private Fields

        private readonly IDbConnectionFactory _connectionFactory;

        #endregion

        #region Constructor

        public DataAccess(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory
                ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        #endregion

        #region Public IDataAccess Methods

        public async Task<int> ExecuteAsync(IDataExecute request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            return await HandleRequest(async conn =>
                await conn.ExecuteAsync(request.GetSql(), request.GetParameters()));
        }

        public async Task<TResponse?> FetchAsync<TResponse>(IDataFetch<TResponse> request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            return await HandleRequest(async conn =>
                await conn.QueryFirstOrDefaultAsync<TResponse>(request.GetSql(), request.GetParameters()));
        }

        public async Task<IEnumerable<TResponse>> FetchListAsync<TResponse>(IDataFetchList<TResponse> request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            return await HandleRequest(async conn =>
                await conn.QueryAsync<TResponse>(request.GetSql(), request.GetParameters()));
        }

        #endregion

        #region Private Helper Method

        private async Task<T> HandleRequest<T>(Func<IDbConnection, Task<T>> funcHandleRequest)
        {
            if (funcHandleRequest is null) throw new ArgumentNullException(nameof(funcHandleRequest));

            using var connection = _connectionFactory.NewConnection();
            connection.Open();
            return await funcHandleRequest.Invoke(connection);
        }

        #endregion
    }
}

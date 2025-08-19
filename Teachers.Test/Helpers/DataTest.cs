using Teachers.Domain.Interfaces;
using Teachers.Domain.Implementation;

namespace Teachers.Test.Helpers
{
    public abstract class DataTest
    {
        protected readonly IDataAccess _dataAccess;

        public DataTest()
        {
            var connectionFactory = new SqlConnectionFactory(Hidden.ConnectionString);

            _dataAccess = new DataAccess(connectionFactory);
        }
    }
}

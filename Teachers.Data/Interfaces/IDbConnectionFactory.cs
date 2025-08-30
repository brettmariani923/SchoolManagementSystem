using System.Data;

namespace Teachers.Data.Interfaces
{
    public interface IDbConnectionFactory
    {
        public IDbConnection NewConnection();
    }
}

using System.Data;

namespace Teachers.Domain.Interfaces
{
    public interface IDbConnectionFactory
    {
        public IDbConnection NewConnection();
    }
}

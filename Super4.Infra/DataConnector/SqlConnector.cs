using Super4.Domain.Interfaces.Repositories.DataConnector;
using System.Data;
using System.Data.SqlClient;

namespace Super4.Infra.DataConnector
{
    public class SqlConnector : IDbConnector
    {

        public SqlConnector(string connectionString)
        {
            dbConnection = SqlClientFactory.Instance.CreateConnection();
            dbConnection.ConnectionString = connectionString;
        }

        public IDbConnection dbConnection { get; }

        public IDbTransaction dbTransaction { get; set; }

        public void Dispose()
        {
            dbConnection?.Dispose();
            dbTransaction?.Dispose();
        }
    }
}

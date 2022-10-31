using System.Data;

namespace Super4.Domain.Interfaces.Repositories.DataConnector
{
    public interface IDbConnector : IDisposable
    {
        IDbConnection dbConnection { get; }
        IDbTransaction dbTransaction { get; set; }
    }
}

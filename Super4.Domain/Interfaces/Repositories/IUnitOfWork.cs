using Super4.Domain.Interfaces.Repositories.DataConnector;

namespace Super4.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        IStockRepository StockRepository { get; }

        public IDbConnector dbConnector { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();


    }
}

using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Repositories.DataConnector;

namespace Super4.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICustomerRepository _customerRepository;

        public UnitOfWork(IDbConnector dbConnector)
        {
            this.dbConnector = dbConnector;
        }

        public ICustomerRepository CustomerRepository => _customerRepository ?? (_customerRepository = new CustomerRepository(dbConnector));

        public IOrderRepository OrderRepository => throw new NotImplementedException();

        public IProductRepository ProductRepository => throw new NotImplementedException();

        public IStockRepository StockRepository => throw new NotImplementedException();

        public IDbConnector dbConnector { get; }





        public void BeginTransaction()
        {
            if (dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnector.dbTransaction = dbConnector.dbConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            }
        }

        public void CommitTransaction()
        {
            if (dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnector.dbTransaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnector.dbTransaction.Rollback();
            }
        }
    }
}

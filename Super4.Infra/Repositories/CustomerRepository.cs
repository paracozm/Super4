using Dapper;
using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Repositories.DataConnector;
using Super4.Domain.Model;

namespace Super4.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnector _dbConnector;
        public CustomerRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task CreateAsync(Customer customer)
        {
            string sql = @"Insert into customer
                            (FirstName, LastName, Document, Street, AddressNumber, Neighborhood, City, Province, CEP)
                            values (@FirstName, @LastName, @Document, @Street, @AddressNumber, @Neighborhood, @City, @Province, @CEP)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Document = customer.Document.Replace("-", "").Replace(".", ""),
                Street = customer.Street,
                AddressNumber = customer.AddressNumber,
                Neighborhood = customer.Neighborhood,
                City = customer.City,
                Province = customer.Province,
                CEP = customer.CEP.Replace("-", "").Replace(".", "")
            }, _dbConnector.dbTransaction);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            string sql = @"select * from customer";

            var customers = await _dbConnector.dbConnection.QueryAsync<Customer>(sql, _dbConnector.dbTransaction);
            return customers.ToList();
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            string sql = @"select * from customer where Id = @Id";

            var customer = await _dbConnector.dbConnection.QueryAsync<Customer>(sql, new { Id = customerId }, _dbConnector.dbTransaction);
            return customer.First();
        }

        public async Task<bool> ExistsById(int customerId)
        {
            string sql = @"select 1 from customer where Id = @Id";

            var customer = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = customerId }, _dbConnector.dbTransaction);
            return customer.FirstOrDefault();
        }

        public async Task<int> GetLastId()
        {
            string sql = "select IDENT_CURRENT('Customer')";

            var result = await _dbConnector.dbConnection.ExecuteScalarAsync(sql, _dbConnector.dbTransaction);
            return Convert.ToInt32(result);
        }


    }
}

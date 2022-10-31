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


        public Task<bool> ExistsById(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CPFExists(string cpf)
        {
            string sql = @"select 1 from customer
                                WHERE Document = @Document ";

            var customerCPF = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Document = cpf }, _dbConnector.dbTransaction);

            return customerCPF.FirstOrDefault();
        }
    }


}

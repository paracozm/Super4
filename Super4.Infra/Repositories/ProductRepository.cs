using Dapper;
using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Repositories.DataConnector;
using Super4.Domain.Model;

namespace Super4.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnector _dbConnector;
        public ProductRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task CreateAsync(Product product)
        {
            string sql = @"insert into product (Id, ProductName) values (@Id, @ProductName)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = product.Id,
                ProductName = product.ProductName
            }, _dbConnector.dbTransaction);
        }

        public async Task UpdateAsync(Product product)
        {
            string sql = $@"update product set ProductName = @ProductName where Id = @Id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = product.Id,
                ProductName = product.ProductName
            }, _dbConnector.dbTransaction);

        }

        public async Task<List<Product>> GetAllAsync()
        {
            string sql = @"select * from product";

            var products = await _dbConnector.dbConnection.QueryAsync<Product>(sql);
            return products.ToList();
        }

        public async Task<Product> GetByIdAsync(string productId)
        {
            string sql = @"select * from product where Id = @Id";

            var product = await _dbConnector.dbConnection.QueryAsync<Product>(sql, new { Id = productId }, _dbConnector.dbTransaction);
            return product.First();

        }

        public async Task<bool> ExistsById(string productId)
        {
            string sql = @"select 1 from product where Id = @Id";

            var product = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = productId }, _dbConnector.dbTransaction);
            return product.FirstOrDefault();
        }
    }
}

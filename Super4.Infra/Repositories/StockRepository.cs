using Dapper;
using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Repositories.DataConnector;
using Super4.Domain.Model;

namespace Super4.Infra.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IDbConnector _dbConnector;
        public StockRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }
        public async Task CreateAsync(Stock stock)
        {
            string sql = "insert into stock (ProductId, Quantity) values (@ProductId, @Quantity)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                ProductId = stock.Product.Id,
                Quantity = stock.Quantity
            }, _dbConnector.dbTransaction);
        }

        public async Task UpdateAsync(Stock stock)
        {
            string sql = $@"update stock set Quantity = @Quantity where ProductId = @Id"; 

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                
                Id = stock.Product.Id,
                Quantity = stock.Quantity
            }, _dbConnector.dbTransaction);

        }

        public async Task<List<Stock>> GetAllAsync()
        {
            string sql = "select s.productId, s.quantity, p.Id, p.productname from stock s inner join product p on s.productid = p.id";

            var stocks = await _dbConnector.dbConnection.QueryAsync<Stock, Product, Stock>(
                sql: sql,
                map: (stock, product) =>
                {
                    stock.Product = product;
                    return stock;
                },
                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);

            return stocks.ToList();
        }

        public async Task<Stock> GetByIdAsync(string productId)
        {
            {
                string sql = "select s.productId, s.quantity, p.Id, p.productname from stock s inner join product p on s.productid = p.id where ProductiD = @Id";

                var product = await _dbConnector.dbConnection.QueryAsync<Stock, Product, Stock>(
                    sql: sql,
                    param: new { Id = productId },
                    map: (stock, product) =>
                    {
                        stock.Product = product;
                        return stock;
                    },
                    splitOn : "Id",
                    transaction:_dbConnector.dbTransaction);
            
                return product.FirstOrDefault();

            }
        }
    }
}

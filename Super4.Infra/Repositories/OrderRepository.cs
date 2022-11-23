using Dapper;
using Super4.Domain.Interfaces.Repositories;
using Super4.Domain.Interfaces.Repositories.DataConnector;
using Super4.Domain.Model;

namespace Super4.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnector _dbConnector;
        public OrderRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }
        public async Task CreateAsync(Order order)
        {
            if (order.Items.Any())
            {
                foreach (var item in order.Items)
                {
                    item.Order = order;
                    await CreateItemAsync(item);
                }
            }


            string sql = $@"INSERT INTO [order] (Id, CustomerId, OrderNumber, OrderDate, TotalPrice) 
                                values (@Id, @CustomerId, @OrderNumber, @OrderDate, @TotalPrice)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = order.Id,
                CustomerId = order.Customer.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            }, _dbConnector.dbTransaction);

            
        }

        public async Task CreateItemAsync(OrderItem item)
        {
            string sql = @"insert into OrderItem (OrderId, ProductId, ProductPrice, TotalAmount)
                                values (@OrderId, @ProductId, @ProductPrice, @TotalAmount)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                OrderId = item.Order.Id,
                ProductId = item.Product.Id,
                ProductPrice = item.ProductPrice,
                TotalAmount = item.TotalAmount
            }, _dbConnector.dbTransaction);
        }

        public async Task<Order> GetByIdAsync(string orderId)
        {
            string sql = @"SELECT o.Id, o.ordernumber, o.orderdate, o.totalprice, c.Id
                            from [order] o
                            join customer c on o.customerId = c.Id";
            var order = await _dbConnector.dbConnection.QueryAsync<Order, Customer, Order>(
                sql: sql,
                map: (order, customer) =>
                {
                    order.Customer = customer;
                    return order;
                },
                param: new {Id = orderId},
                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);

            return order.First();
            
        }

        public async Task<List<Order>> GetAllAsync()
        {
            string sql = @"SELECT o.Id
                            ,o.ordernumber
                            ,o.orderdate
                            ,o.totalprice
                            ,c.Id
                            ,oi.orderId as Id
                            ,oi.productprice
                            ,oi.totalamount
                            ,p.Id
                            ,p.productName
                            ,s.productId as Id
                            ,s.quantity
                            FROM [order] o
                            JOIN Customer c ON o.customerId = c.Id
                            JOIN OrderItem oi ON oi.orderId = o.Id
                            JOIN Product p ON oi.ProductId = p.Id
                            JOIN Stock s on s.productId = p.Id";
            var orders = await _dbConnector.dbConnection.QueryAsync<Order, Customer, OrderItem, Product, Stock, Order>(
                sql: sql,
                map: (order, customer, orderItem, product, stock) =>
                {
                    
                    order.Stock = stock;
                    order.Item = orderItem;
                    order.Product = product;
                    order.Customer = customer;
                    return order;
                },

                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);


            return orders.ToList();
        }

        public async Task<List<OrderItem>> GetItemByOrderIdAsync(string orderId)
        {
            string sql = $@"SELECT oi.orderId as Id, oi.productprice, oi.totalamount, o.Id, o.totalprice, p.Id, p.productname, s.productId as Id, s.quantity
from orderitem oi
join product p on oi.productid = p.Id
join [order] o on oi.orderid = o.Id
join stock s on oi.productid = s.productid
where oi.orderid = @OrderId";

            var items = await _dbConnector.dbConnection.QueryAsync<OrderItem, Order, Product, Stock, OrderItem>(

                sql: sql,
                map: (item, order, product, stock) =>
                {
                    item.Stock = stock;
                    item.Order = order;
                    item.Product = product;
                    return item;
                },
                param: new { OrderId = orderId },
                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);
            return items.ToList();
        }

        public async Task<bool> ExistsById(string orderId)
        {
            string sql = $@"SELECT 1 FROM [Order] WHERE Id = @Id ";

            var order = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = orderId }, _dbConnector.dbTransaction);

            return order.FirstOrDefault();
        }

        
    }
}

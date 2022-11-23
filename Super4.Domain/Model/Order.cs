namespace Super4.Domain.Model
{
    public class Order
    {
        public string Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Customer Customer { get; set; } //customerId
        public Product Product { get; set; } //productId
        public OrderItem Item { get; set; } //orderItem
        public Stock Stock { get; set; } //stock
        public List<OrderItem> Items { get; set; }
    }
}

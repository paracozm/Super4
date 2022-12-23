namespace Super4.Domain.Model
{
    public class Stock
    {
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderItem Item { get; set; }
        public Order Order { get; set; }
    }
}

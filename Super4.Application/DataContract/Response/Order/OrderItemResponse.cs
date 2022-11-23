namespace Super4.Application.DataContract.Response.Order
{
    public sealed class OrderItemResponse
    {
        public string ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int TotalAmount { get; set; }
    }
}

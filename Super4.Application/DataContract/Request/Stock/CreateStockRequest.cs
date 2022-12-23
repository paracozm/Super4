namespace Super4.Application.DataContract.Request.Stock
{
    public sealed class CreateStockRequest
    {
        public string ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}

namespace Super4.Application.DataContract.Request.Stock
{
    public sealed class CreateStockRequest
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

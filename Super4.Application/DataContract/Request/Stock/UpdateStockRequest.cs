namespace Super4.Application.DataContract.Request.Stock
{
    public sealed class UpdateStockRequest
    {
        public string ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}

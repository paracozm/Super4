using Super4.Domain.Model;

namespace Super4.Application.DataContract.Request.Order
{
    public sealed class CreateOrderItemRequest
    {
        public string ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int TotalAmount { get; set; }

    }
}

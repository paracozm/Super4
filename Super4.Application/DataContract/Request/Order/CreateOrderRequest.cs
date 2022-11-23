

namespace Super4.Application.DataContract.Request.Order
{
    public sealed class CreateOrderRequest
    {
        public int CustomerId { get; set; } //FK
        //public decimal TotalPrice { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; }
    }
}

namespace Super4.Application.DataContract.Request.Order
{
    public sealed class FillOrder
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string CEP { get; set; }
        public string AddressNumber { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; }
    }
}

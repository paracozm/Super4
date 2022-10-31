namespace Super4.Domain.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Street { get; set; }
        public string AddressNumber { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string CEP { get; set; }
    }
}

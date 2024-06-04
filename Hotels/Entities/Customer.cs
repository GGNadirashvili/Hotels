namespace Hotels.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int Number{ get; set; }
        public string? CustomerGuid { get; set; }

        public List<Booking>? Bookings { get; set; }
    }
}

namespace Hotels.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();

    }
}

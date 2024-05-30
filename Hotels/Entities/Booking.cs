namespace Hotels.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public Customer? Customer { get; set; }
        public Room? Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public string? HotelId { get; set; }
        public string? RoomId { get; set; }
    }
}

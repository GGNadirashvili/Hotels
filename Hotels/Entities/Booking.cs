namespace Hotels.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int? HotelId { get; set; }
        public string? CustomerGuid { get; set; }
        public int? RoomId { get; set; }
        public bool IsBooked { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Customer? Customer { get; set; }
        public Room? Room { get; set; }
        public Hotel? Hotel { get; set; }

    }
}

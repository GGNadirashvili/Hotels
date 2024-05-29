namespace Hotels.Entities
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string? Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; } = new City();
        public List<Room> Rooms { get; set; } = new List<Room>();
        public string? PhotoUrl { get; set; }

    }
}

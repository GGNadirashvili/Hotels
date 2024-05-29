﻿namespace Hotels.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public string? PhotoUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public Hotel? Hotel { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
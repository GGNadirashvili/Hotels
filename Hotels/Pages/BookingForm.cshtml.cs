using Hotels.Data;
using Hotels.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hotels.Pages
{
    [Authorize]
    public class BookingFormModel : PageModel
    {
        private readonly ApplicationDbContext db;

        [BindProperty]
        public Booking Booking { get; set; }

        [BindProperty]
        public List<Hotel> Hotels { get; set; }

        [BindProperty]
        public List<Room> Rooms { get; set; }

        public string RoomNumber { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public BookingFormModel(ApplicationDbContext db)
        {
            this.db = db;
            Booking = new Booking();
            Hotels = new List<Hotel>();
            Rooms = new List<Room>();
            RoomNumber = string.Empty;
            HotelName = string.Empty;
            City = string.Empty;
        }
        public async Task OnGetAsync(int? hotelId, int? roomId, decimal price)
        {
            Hotels = await db.Hotels.ToListAsync();
            Rooms = await db.Rooms.ToListAsync();
            var hotel = await db.Hotels.FirstOrDefaultAsync(x => x.HotelId == hotelId!.Value);

            if (hotelId.HasValue)
            {
                Booking.HotelId = hotelId.Value;
                HotelName = hotel.Name!;
            }

            if (roomId.HasValue)
            {
                Booking.RoomId = roomId.Value;
                RoomNumber = hotel.Rooms.FirstOrDefault(x=>x!.RoomId == roomId).RoomNumber;
                Price = hotel.Rooms.FirstOrDefault(x => x.RoomId == roomId).UnitPrice;
            }
            var customerGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Booking.CustomerGuid = customerGuid;
            Booking.TotalPrice = price;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await OnGetAsync(Booking.HotelId, Booking.RoomId, Booking.TotalPrice);
            db.Bookings.Add(Booking);

            var room = await db.Rooms.FindAsync(Booking.RoomId);
            Booking.IsBooked = true;
            room!.IsBooked = true;

            await db.SaveChangesAsync();

            return RedirectToPage("./Bookings");
        }
    }
}

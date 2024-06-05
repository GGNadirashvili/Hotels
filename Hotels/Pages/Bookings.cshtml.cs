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
    public class BookingsModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public List<Booking> Bookings { get; set; }
        [BindProperty]
        public List<Room> Rooms { get; set; }

        public List<CustomerBookings>CustomerBookings { get; set; }

        public BookingsModel(ApplicationDbContext db)
        {
            this.db = db;
            Bookings = new List<Booking>();
            Rooms = new List<Room>();
            CustomerBookings = new List<CustomerBookings>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var customerGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = from booking in db.Bookings.Include(b => b.Hotel).Include(b => b.Room).Include(b => b.Hotel!.City)
                        join customer in db.Customers on booking.CustomerGuid equals customer.CustomerGuid
                        where booking.CustomerGuid == customerGuid
                        select new CustomerBookings { Bookings = booking, Customer = customer };

            CustomerBookings = await query.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int RoomId)
        {
            var booking = await db.Bookings
                .Include(r => r.Room)
                .FirstOrDefaultAsync(x => x.RoomId == RoomId);

            if (booking != null)
            {
                db.Bookings.Remove(booking);
                booking.Room!.IsBooked = false;

                await db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}

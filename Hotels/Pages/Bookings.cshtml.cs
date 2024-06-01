using Hotels.Data;
using Hotels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Pages
{
    public class BookingsModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public List<Booking> Bookings;

         public BookingsModel(ApplicationDbContext db)
    {
        this.db = db;
        Bookings = new List<Booking>();
    }

        public async Task<IActionResult> OnGetAsync()
        {
            Bookings = await db.Bookings.Include(h => h.Hotel).Include(c => c.Hotel!.City).Include(c => c.Customer).Include(r => r.Room).ToListAsync();
            return Page();
        }
    }
}

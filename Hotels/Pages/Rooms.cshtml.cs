using Hotels.Data;
using Hotels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Pages
{
    public class RoomsModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public RoomsModel(ApplicationDbContext db)
        {
            this.db = db;
            Rooms = new List<Room>();
        }

        public IList<Room> Rooms { get; set; }

        public async Task<IActionResult> OnGetAsync(int hotelId)
        {
            Rooms = await db.Rooms.Where(r => r.Hotel!.HotelId == hotelId).ToListAsync();
            return Page();

        }
    }
}

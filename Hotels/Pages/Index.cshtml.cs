using Hotels.Data;
using Hotels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hotels.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
            FavoriteRooms = new List<Room>();
        }

        public IList<Room> FavoriteRooms { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var CustomerGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            FavoriteRooms = await db.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Bookings)
                .Where(r => r.IsFavorite)
                .ToListAsync();
            return Page();
        }
    }
}

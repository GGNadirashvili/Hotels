using Hotels.Data;
using Hotels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
            FavoriteRooms = await db.Rooms.Where(r => r.IsFavorite).Include(r => r.Hotel).ToListAsync();
            return Page();
        }
    }
}

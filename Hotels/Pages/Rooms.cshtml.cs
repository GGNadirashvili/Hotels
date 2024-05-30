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
            RoomTypes = new List<string>();

        }

        public IList<Room> Rooms { get; set; }
        public IList<string> RoomTypes { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? RoomType { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? CheckIn { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? CheckOut { get; set; }

        [BindProperty]
        public int RoomId { get; set; }

        [FromQuery]
        public int HotelId { get; set; }
        public async Task<IActionResult> OnGetAsync(int hotelId)
        {
            var query = db.Rooms
                .Where(r => r.Hotel!.HotelId == hotelId)
                .Include(r => r.RoomType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(RoomType))
            {
                query = query.Where(r => r.RoomType!.TypeName == RoomType);
            }

            if (MinPrice.HasValue)
            {
                query = query.Where(r => r.UnitPrice >= MinPrice.Value);
            }

            if (MaxPrice.HasValue)
            {
                query = query.Where(r => r.UnitPrice <= MaxPrice.Value);
            }

            Rooms = await query.ToListAsync();
            RoomTypes = await db.RoomTypes.Select(rt => rt.TypeName!).Distinct().ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var room = await db.Rooms.FirstOrDefaultAsync(r => r.RoomId == RoomId);

            if (room != null)
            {
                room.IsFavorite = !room.IsFavorite;

                await db.SaveChangesAsync();
            }

            return RedirectToPage(new { hotelId = HotelId });
        }

    }
}

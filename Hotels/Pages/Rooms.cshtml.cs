using Hotels.Data;
using Hotels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        [BindProperty]
        public string? RoomType { get; set; }

        [BindProperty]
        public int RoomId { get; set; }

        [FromQuery]
        public int HotelId { get; set; }

        [BindProperty]
        public int MinPrice { get; set; }
        [BindProperty]
        public int MaxPrice { get; set; }
        public async Task<IActionResult> OnGetAsync(int hotelId, int minPrice, int maxPrice, string? roomType)

        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            RoomTypes = await db.RoomTypes.Select(rt => rt.TypeName!).Distinct().ToListAsync();


            IQueryable<Room> room = db.Rooms
                .Include(r => r.RoomType)
                .Where(r => r.HotelId == hotelId);

            if (maxPrice > minPrice)
            {
                room = room.Where(r => r.UnitPrice >= minPrice && r.UnitPrice <= maxPrice);
            }

            if (!string.IsNullOrEmpty(roomType))
            {
                room = room.Where(r => r.RoomType!.TypeName == roomType);
            }

            Rooms = await room.ToListAsync();

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

            return RedirectToPage(new { hotelId = HotelId, minPrice = MinPrice, maxPrice = MaxPrice, roomType = RoomType });
        }
        public async Task<IActionResult> OnPostButtonClick(int hotelId)
        {
            Rooms = await db.Rooms
            .Include(r => r.RoomType)
            .Where(r => r.HotelId == hotelId)
            .ToListAsync();

            HotelId = hotelId;
            MinPrice = 0;
            MaxPrice = 0;
            RoomType = null;

            return RedirectToPage(new { hotelId = HotelId, minPrice = MinPrice, maxPrice = MaxPrice, roomType = RoomType });
        }
    }
}

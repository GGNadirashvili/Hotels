using Hotels.Data;
using Hotels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        public string? MinPrice { get; set; }
        [BindProperty]
        public string? MaxPrice { get; set; }
        public async Task<IActionResult> OnGetAsync(int hotelId, string minPrice, string maxPrice, string? roomType)

        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            RoomTypes = await db.RoomTypes.Select(rt => rt.TypeName!).Distinct().ToListAsync();


            IQueryable<Room> room = db.Rooms
                .Include(r => r.RoomType)
                .Where(r => r.HotelId == hotelId);

            if (!string.IsNullOrEmpty(maxPrice) && !string.IsNullOrEmpty(minPrice))
            {
                int minPriceInt = int.Parse(minPrice);
                int maxPriceInt = int.Parse(maxPrice);
                if (maxPriceInt > minPriceInt)
                {
                    room = room.Where(r => r.UnitPrice >= minPriceInt && r.UnitPrice <= maxPriceInt);
                }
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
            var customerGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var room = await db.Rooms.FirstOrDefaultAsync(r => r.RoomId == RoomId);

            if (room != null)
            {
                room.IsFavorite = !room.IsFavorite;

                if (room.IsFavorite)
                {
                    room.CustomerGuid = customerGuid;
                }
                else if (room.IsFavorite is false)
                {
                    room.CustomerGuid = "";
                }

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
            MinPrice = "";
            MaxPrice = "";
            RoomType = null;

            return RedirectToPage(new { hotelId = HotelId, minPrice = MinPrice, maxPrice = MaxPrice, roomType = RoomType });
        }
    }
}

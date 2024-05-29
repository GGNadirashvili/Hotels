using Hotels.Data;
using Hotels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Pages
{
    public class HotelsModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public HotelsModel(ApplicationDbContext db)
        {
            this.db = db;
            Hotels = new List<Hotel>();
            Cities = new List<City>();
        }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCityId { get; set; }
        public IList<Hotel> Hotels { get; set; }
        public IList<City> Cities { get; set; }

        public async Task OnGetAsync()
        {
            Cities = await db.Cities.ToListAsync();

            IQueryable<Hotel> hotels = db.Hotels.Include(h => h.City).Include(h => h.Rooms);

            if (SelectedCityId.HasValue)
            {
                hotels = hotels.Where(h => h.CityId == SelectedCityId.Value);
            }

            Hotels = await hotels.ToListAsync();
        }
    }
}


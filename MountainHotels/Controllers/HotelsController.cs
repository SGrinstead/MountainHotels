using Microsoft.AspNetCore.Mvc;
using MountainHotels.DataAccess;
using MountainHotels.Models;

namespace MountainHotels.Controllers
{
    public class HotelsController : Controller
    {
        private readonly MountainHotelsContext _context;

        public HotelsController(MountainHotelsContext context)
        {
            _context = context;
        }

        // GET: /Hotels
        public IActionResult Index(string? location, int? year)
        {
            var hotels = _context.Hotels.AsEnumerable();
            if(location != null)
            {
                hotels = hotels.Where(h => h.Location == location);
                ViewData["SearchLocation"] = location;
            }
            if(year != null)
            {
                hotels = hotels.Where(h => h.YearBuilt == year);
                ViewData["SearchYear"] = year;
            }
            ViewData["AllLocations"] = _context.Hotels.Select(h => h.Location).Distinct().ToList();
            ViewData["AllYears"] = _context.Hotels.Select(h => h.YearBuilt).Distinct().ToList();
            return View(hotels);
        }

        [Route("/hotels/{hotelId:int}")]
        public IActionResult Show(int hotelId)
        {
            var hotel = _context.Hotels.Find(hotelId);
            return View(hotel);
        }

        // GET: /Hotels/New
        public IActionResult New()
        {
            return View();
        }

        [Route("/hotels/{hotelId:int}/edit")]
        public IActionResult Edit(int hotelId)
        {
            var hotel = _context.Hotels.Find(hotelId);
            return View(hotel);
        }

        [HttpPost]
        [Route("/hotels/{hotelId:int}")]
        public IActionResult Update(int hotelId, Hotel hotel)
        {
            hotel.Id = hotelId;
            _context.Hotels.Update(hotel);
            _context.SaveChanges();
            return Redirect($"/hotels/{hotelId}");
        }

        // POST: /Hotels
        [HttpPost]
        public IActionResult Index(Hotel hotel)
        {
            //Take the movie sent in the request and save it to the database
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            // Redirect back to the index page with all hotels
            return RedirectToAction("Index");
        }
    }
}

using System.Diagnostics;
using Flavour_Fiesta.Models;
using Microsoft.AspNetCore.Mvc;
using Flavour_Fiesta.Data;
using Microsoft.EntityFrameworkCore;

namespace Flavour_Fiesta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Async Index: Supports filter + search
        public async Task<IActionResult> Menu(string category, string query)
        {
            var items = await GetFilteredItemsAsync(category, query);
            return View("Menu",items);
        }

        // Async Filtering + Search
        private async Task<List<FoodItem>> GetFilteredItemsAsync(string category, string query)
        {
            var items = _context.FoodItems.AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                items = items.Where(x => x.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
                items = items.Where(x => x.Name.Contains(query) || x.Category.Contains(query));
            }

            return await items.ToListAsync();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

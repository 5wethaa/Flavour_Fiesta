using Flavour_Fiesta.Domain.Models;
using Flavour_Fiesta.Service.Interfaces;
using Flavour_Fiesta.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Flavour_Fiesta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFoodItemService _foodItemService;

        public HomeController(ILogger<HomeController> logger, IFoodItemService foodItemService)
        {
            _logger = logger;
            _foodItemService = foodItemService;
        }

        // Async Index: filter + search
        public async Task<IActionResult> Menu(string category, string query)
        {
            var items = await _foodItemService.GetFilteredItemsAsync(category, query);
            return View("Menu", items);
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

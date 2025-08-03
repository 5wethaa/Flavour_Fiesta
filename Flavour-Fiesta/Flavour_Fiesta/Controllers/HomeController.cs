
using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;
using Flavour_Fiesta.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Flavour_Fiesta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFoodItemService _foodItemService;
        private readonly IFeedbackService _feedbackService;

        public HomeController(ILogger<HomeController> logger, IFoodItemService foodItemService, IFeedbackService feedbackService)
        {
            _logger = logger;
            _foodItemService = foodItemService;
            _feedbackService = feedbackService;
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

        [HttpGet]
        public async Task<IActionResult> Feedback()
        {
            var vm = new FeedbackPageViewModel
            {
                FeedbackList = await _feedbackService.GetAllFeedbackAsync()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedbackAjax([FromBody] FeedbackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feedback = new Feedback
            {
                Name = model.Name,
                Email = model.Email,
                Rating = model.Rating,
                Comments = model.Comments,
                SubmittedAt = DateTime.UtcNow
            };

            await _feedbackService.SaveFeedbackAsync(feedback);

            return Json(new { success = true, message = "Feedback submitted successfully." });
        }


        [HttpGet]
        public async Task<IActionResult> FeedbackListPartial()
        {
            var feedbacks = await _feedbackService.GetAllFeedbackAsync();
            return PartialView("_FeedbackListPartial", feedbacks);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

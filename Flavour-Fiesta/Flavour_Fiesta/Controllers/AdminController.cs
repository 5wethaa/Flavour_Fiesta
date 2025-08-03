
using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;
using Flavour_Fiesta.Models;

using Microsoft.AspNetCore.Mvc;

namespace Flavour_Fiesta.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFoodItemService _foodItemService;
        private readonly IWebHostEnvironment _env;

        public AdminController(IFoodItemService foodItemService, IWebHostEnvironment env)
        {
            _foodItemService = foodItemService;
            _env = env;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View(new AdminLoginViewModel());
        }

        [HttpPost]
        public IActionResult AdminLogin(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Dashboard", model);
            }

            var envEmail = Environment.GetEnvironmentVariable("AdminEmail");
            var envPassword = Environment.GetEnvironmentVariable("AdminPassword");

            if (model.Email == envEmail && model.Password == envPassword)
            {
                HttpContext.Session.SetString("IsAdminLoggedIn", "true");
                return RedirectToAction("FoodList");
            }

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View("Dashboard", model);
        }

        public async Task<IActionResult> FoodList()
        {
            if (HttpContext.Session.GetString("IsAdminLoggedIn") != "true")
                return RedirectToAction("Dashboard");

            var items = await _foodItemService.GetAllAsync();
            return View(items);
        }

        public IActionResult AdminLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Dashboard");
        }

        // GET: Add Food View
        [HttpGet]
        public IActionResult AddFood()
        {
            if (HttpContext.Session.GetString("IsAdminLoggedIn") != "true")
                return RedirectToAction("Dashboard");

            return View(new FoodItem());
        }

        // POST: Add Food
        [HttpPost]
        public async Task<IActionResult> AddFood(FoodItem item, IFormFile imageFile)
        {
            if (HttpContext.Session.GetString("IsAdminLoggedIn") != "true")
                return RedirectToAction("Dashboard");

            // Server-side validation
            if (!ModelState.IsValid)
                return View(item);

            if (imageFile == null || imageFile.Length == 0)
            {
                ViewBag.ImageError = "Please select an image.";
                return View(item);
            }

            // Image saving
            var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            item.ImageUrl = "/images/" + uniqueFileName;

            // Add to database
            await _foodItemService.AddAsync(item);
            TempData["SuccessMessage"] = "Food item added successfully!";
            return RedirectToAction("FoodList");
        }

        public async Task<IActionResult> EditFood(int id)
        {
            var item = await _foodItemService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> EditFood(FoodItem updatedItem, IFormFile? imageFile)
        {
            var existingItem = await _foodItemService.GetByIdAsync(updatedItem.Id);
            if (existingItem == null) return NotFound();

            existingItem.Name = updatedItem.Name;
            existingItem.Category = updatedItem.Category;
            existingItem.Price = updatedItem.Price;
            existingItem.Quantity = updatedItem.Quantity;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingItem.ImageUrl = "/images/" + uniqueFileName;
            }

            if (ModelState.IsValid)
            {
                await _foodItemService.UpdateAsync(existingItem);
                TempData["SuccessMessage"] = "Item updated!";
                return RedirectToAction("FoodList");
            }

            return View(existingItem);
        }

        public async Task<IActionResult> DeleteFood(int id)
        {
            await _foodItemService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Item deleted!";
            return RedirectToAction("FoodList");
        }
    }
}


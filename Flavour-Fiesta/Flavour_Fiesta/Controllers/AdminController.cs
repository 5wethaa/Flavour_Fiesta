//using Flavour_Fiesta.Domain.Interfaces;
//using Flavour_Fiesta.Domain.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace Flavour_Fiesta.Controllers
//{
//    public class AdminController : Controller
//    {
//        private readonly IFoodItemService _foodItemService;

//        public AdminController(IFoodItemService foodItemService)
//        {
//            _foodItemService = foodItemService;
//        }

//        [HttpGet, HttpPost]
//        public IActionResult Dashboard()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult AdminLogin(string email, string password)
//        {
//            var envEmail = Environment.GetEnvironmentVariable("AdminEmail");
//            var envPassword = Environment.GetEnvironmentVariable("AdminPassword");

//            if (email == envEmail && password == envPassword)
//            {
//                HttpContext.Session.SetString("IsAdminLoggedIn", "true");
//                return RedirectToAction("FoodList");
//            }

//            ViewBag.Error = "Invalid email or password.";
//            return View("Dashboard");
//        }

//        public async Task<IActionResult> FoodList()
//        {
//            if (HttpContext.Session.GetString("IsAdminLoggedIn") != "true")
//                return RedirectToAction("Dashboard");

//            var items = await _foodItemService.GetAllAsync();
//            return View(items);
//        }

//        public IActionResult AdminLogout()
//        {
//            HttpContext.Session.Clear();
//            return RedirectToAction("Dashboard");
//        }

//        public IActionResult AddFood() => View();

//        [HttpPost]
//        public async Task<IActionResult> AddFood(FoodItem item)
//        {
//            if (ModelState.IsValid)
//            {
//                await _foodItemService.AddAsync(item);
//                TempData["SuccessMessage"] = "Item added successfully!";
//                return RedirectToAction("FoodList");
//            }
//            return View(item);
//        }

//        public async Task<IActionResult> EditFood(int id)
//        {
//            var item = await _foodItemService.GetByIdAsync(id);
//            if (item == null) return NotFound();
//            return View(item);
//        }

//         [HttpPost]
//        public async Task<IActionResult> EditFood(FoodItem updatedItem)
//        {
//            if (ModelState.IsValid)
//            {
//                await _foodItemService.UpdateAsync(updatedItem);
//                TempData["SuccessMessage"] = "Item updated!";
//                return RedirectToAction("FoodList");
//            }
//            return View(updatedItem);
//        }

//        public async Task<IActionResult> DeleteFood(int id)
//        {
//            await _foodItemService.DeleteAsync(id);
//            TempData["SuccessMessage"] = "Item deleted!";
//            return RedirectToAction("FoodList");
//        }
//    }
//}


using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;
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

        [HttpGet, HttpPost]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string email, string password)
        {
            var envEmail = Environment.GetEnvironmentVariable("AdminEmail");
            var envPassword = Environment.GetEnvironmentVariable("AdminPassword");

            if (email == envEmail && password == envPassword)
            {
                HttpContext.Session.SetString("IsAdminLoggedIn", "true");
                return RedirectToAction("FoodList");
            }

            ViewBag.Error = "Invalid email or password.";
            return View("Dashboard");
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

        public IActionResult AddFood() => View();

        [HttpPost]
        public async Task<IActionResult> AddFood(FoodItem item, IFormFile imageFile)
        {
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

                item.ImageUrl = "/images/" + uniqueFileName;
            }

            await _foodItemService.AddAsync(item);
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
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                updatedItem.ImageUrl = "/images/" + uniqueFileName;
            }

            if (ModelState.IsValid)
            {
                await _foodItemService.UpdateAsync(updatedItem);
                TempData["SuccessMessage"] = "Item updated!";
                return RedirectToAction("FoodList");
            }

            return View(updatedItem);
        }

        public async Task<IActionResult> DeleteFood(int id)
        {
            await _foodItemService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Item deleted!";
            return RedirectToAction("FoodList");
        }
    }
}

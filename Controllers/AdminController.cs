using Flavour_Fiesta.Data;
using Flavour_Fiesta.Models;
using Microsoft.AspNetCore.Mvc;

namespace Flavour_Fiesta.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [HttpPost]
        public IActionResult Dashboard()
        {
            return View(); 
        }



        [HttpPost]
        public IActionResult AdminLogin(string email, string password)
        {
            // Read env variables
            var envEmail = Environment.GetEnvironmentVariable("AdminEmail");
            var envPassword = Environment.GetEnvironmentVariable("AdminPassword");
            Console.WriteLine($"Email: {email}, Password: {password}");
            // Match credentials
            if (email == envEmail && password == envPassword)
            {
                HttpContext.Session.SetString("IsAdminLoggedIn", "true");
                return RedirectToAction("FoodList","Admin");
            }

            ViewBag.Error = "Invalid email or password.";
            return View("Dashboard");
        }

        

        // List all food items
        public IActionResult FoodList()
        {

            var LoggedIn = HttpContext.Session.GetString("IsAdminLoggedIn");
            if (LoggedIn != "true")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            var allItems = _context.FoodItems.ToList();
            return View(allItems);
        }

        public IActionResult AdminLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Dashboard", "Admin");
        }

        // CREATE 
        public IActionResult AddFood()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        public IActionResult AddFood(FoodItem item)
        {
            if (ModelState.IsValid)
            {
                _context.FoodItems.Add(item);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Item added successfully!";
                return RedirectToAction("FoodList");
            }
            return View(item);
        }

        // EDIT - GET
        public IActionResult EditFood(int id)
        {
            var item = _context.FoodItems.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // EDIT - POST
        [HttpPost]
        public IActionResult EditFood(FoodItem updatedItem)
        {
            if (ModelState.IsValid)
            {
                _context.FoodItems.Update(updatedItem);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Item updated!";
                return RedirectToAction("FoodList");
            }
            return View(updatedItem);
        }

        // DELETE
        public IActionResult DeleteFood(int id)
        {
            var item = _context.FoodItems.Find(id);
            if (item != null)
            {
                _context.FoodItems.Remove(item);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Item deleted!";
            }
            return RedirectToAction("FoodList");
        }

      
        

        

        
    }
}

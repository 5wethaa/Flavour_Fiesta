using Flavour_Fiesta.Models;
using Microsoft.AspNetCore.Mvc;
using Flavour_Fiesta.Data;


namespace Flavour_Fiesta.Controllers
{
    public class CustController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer Customers)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Customers.FirstOrDefault(u => u.Email == Customers.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email already registered.");
                    return View(Customers);
                }

                // Set default values
                Customers.IsConfirmed = true;

                // Save user to database
                _context.Customers.Add(Customers);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Registered successfully! Please login.";

                // Redirect to login page
                return RedirectToAction("Login", "Cust" +
                    "" +
                    "" +
                    "" +
                    ""); ;
            }

            // If validation fails
            ViewBag.Message = "Registration failed. Please check the form.";
            return View(Customers);
        }

        // GET: /Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Customers
                        .FirstOrDefault(u => u.Email == email && u.Password == password && u.IsConfirmed);

            if (user == null)
            {

                ViewBag.Message = " Email not found.";
                return View();
            }

            if (user.Password != password)
            {
                ViewBag.Message = " Incorrect password.";
                return View();
            }

            if (!user.IsConfirmed)
            {
                ViewBag.Message = " Please Register your account before login.";
                return View();
            }

            // Successful login
            HttpContext.Session.SetString("CustomerId", user.Id.ToString());
            HttpContext.Session.SetString("UserEmail", user.Email);
            return RedirectToAction("Menu", "Home");
        }
    }
}

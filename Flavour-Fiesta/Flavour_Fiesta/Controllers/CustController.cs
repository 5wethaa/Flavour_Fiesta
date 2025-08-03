
using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;
using Flavour_Fiesta.Models;
using Microsoft.AspNetCore.Mvc;


namespace Flavour_Fiesta.Controllers
{
    public class CustController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustController> _logger;

        public CustController(ICustomerService customerService, ILogger<CustController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        //REGISTER 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            var customer = new Customer
            {
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };

            try
            {
                if (!_customerService.Register(customer, out string message))
                {
                    ModelState.AddModelError("Email", message); // e.g., "Email already registered"
                    return View(model);
                }

                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed for {Email}", model.Email);
                ModelState.AddModelError("", "Unexpected error — please try again later.");
                return View(model);
            }
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = _customerService.Login(model.Email, model.Password, out string message);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, message); // e.g., "Invalid password" or "User not found"
                    return View(model);
                }

                HttpContext.Session.SetString("CustomerId", user.Id.ToString());
                HttpContext.Session.SetString("UserEmail", user.Email);

                return RedirectToAction("Menu", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for {Email}", model.Email);
                ModelState.AddModelError("", "Unable to log in right now. Please try again later.");
                return View(model);
            }
        }
    }
}


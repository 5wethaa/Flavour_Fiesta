using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Flavour_Fiesta.Controllers
{
    public class CustController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustController> _logger;

        public CustController(ICustomerService customerService,
                              ILogger<CustController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            if (!ModelState.IsValid)
                return View(model);
            try
            {
                if (!_customerService.Register(model, out string message))
                {
                    ModelState.AddModelError("Email", message);
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var user = _customerService.Login(email, password, out string message);
                if (user == null)
                {
                    ViewBag.Message = message;
                    return View();
                }

                HttpContext.Session.SetString("CustomerId", user.Id.ToString());
                HttpContext.Session.SetString("UserEmail", user.Email);
                return RedirectToAction("Menu", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for {Email}", email);
                ViewBag.Message = "Unable to log in right now. Please try again later.";
                return View();
            }

        }



        
    }
}

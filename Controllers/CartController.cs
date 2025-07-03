using Flavour_Fiesta.Data;
using Flavour_Fiesta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flavour_Fiesta.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  Add to Cart
        [HttpPost]
        public IActionResult AddToCart(int foodItemId)
        {
            var customerIdStr = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerIdStr))
            {
                return RedirectToAction("Login", "Cust");
            }

            int customerId = int.Parse(customerIdStr);

            // Check if item already in cart
            var existingItem = _context.CartItems
                .FirstOrDefault(c => c.FoodItemId == foodItemId && c.CustomerId == customerId);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                var cartItem = new CartItem
                {
                    FoodItemId = foodItemId,
                    CustomerId = customerId,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }

            _context.SaveChanges();
            TempData["Toast"] = "Item added to cart!";
            return RedirectToAction("Menu","Home");
        }

        // View Cart
        public IActionResult ViewCart()
        {
            var customerIdStr = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerIdStr))
            {
                return RedirectToAction("Login", "Cust");
            }

            int customerId = int.Parse(customerIdStr);

            var cartItems = _context.CartItems
                .Include(c => c.FoodItem)
                .Where(c => c.CustomerId == customerId)
                .ToList();

            return View(cartItems);
        }

        //  Remove Item from Cart
        [HttpPost]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var item = _context.CartItems.Find(cartItemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("ViewCart");
        }

        //  Checkout Page
        public IActionResult Checkout()
        {

            var customerIdStr = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerIdStr))
            {
                return RedirectToAction("Login", "Cust");
            }

            int customerId = int.Parse(customerIdStr);

            var cartItems = _context.CartItems
                .Include(c => c.FoodItem)
                .Where(c => c.CustomerId == customerId)
                .ToList();

            ViewBag.Total = cartItems.Sum(c => c.FoodItem.Price * c.Quantity);
            return View(cartItems);
        }
    }
}


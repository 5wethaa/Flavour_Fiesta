using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Flavour_Fiesta.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartItemService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartItemService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int foodItemId)
        {
            var customerIdStr = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerIdStr))
                return RedirectToAction("Login", "Cust");

            try
            {
                int customerId = int.Parse(customerIdStr);
                await _cartService.AddOrUpdateAsync(customerId, foodItemId);
                TempData["Toast"] = "Item added to cart!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in adding item to cart for foodItemId {foodItemId}", foodItemId);
                TempData["Toast"] = "Error : Please try again.";
            }

            return RedirectToAction("Menu", "Home");
        }

        public async Task<IActionResult> ViewCart()
        {
            var customerIdStr = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerIdStr))
                return RedirectToAction("Login", "Cust");

            try
            {
                int customerId = int.Parse(customerIdStr);
                var items = await _cartService.GetCartItemsAsync(customerId);
                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve cart items.");
                TempData["Toast"] = "Unable to load cart.";
                return RedirectToAction("Menu", "Home");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                await _cartService.RemoveAsync(cartItemId);
                TempData["Toast"] = "Item removed from cart.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to remove cart item {cartItemId}", cartItemId);
                TempData["Toast"] = "An error occurred. Please try again.";
            }

            return RedirectToAction("ViewCart");
        }

        public async Task<IActionResult> Checkout()
        {
            var customerIdStr = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerIdStr))
                return RedirectToAction("Login", "Cust");


            try
            {
                int customerId = int.Parse(customerIdStr);
                var items = await _cartService.GetCartItemsAsync(customerId);
                ViewBag.Total = await _cartService.CalculateTotalAsync(customerId);

                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Checkout failed.");
                TempData["Toast"] = "Something went wrong. Please try again later.";
                return RedirectToAction("Menu", "Home");
            }
        }
    }
}

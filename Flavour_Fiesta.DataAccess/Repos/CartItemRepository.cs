using Flavour_Fiesta.Data;
using Flavour_Fiesta.DataAccess.Interfaces;
using Flavour_Fiesta.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Flavour_Fiesta.DataAccess.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int customerId)
        {
            return await _context.CartItems
                .Include(ci => ci.FoodItem)
                .Where(ci => ci.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task AddOrUpdateAsync(int customerId, int foodItemId)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.FoodItemId == foodItemId);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                var newItem = new CartItem
                {
                    CustomerId = customerId,
                    FoodItemId = foodItemId,
                    Quantity = 1
                };
                _context.CartItems.Add(newItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> CalculateTotalAsync(int customerId)
        {
            return await _context.CartItems
                .Include(ci => ci.FoodItem)
                .Where(ci => ci.CustomerId == customerId)
                .SumAsync(ci => ci.FoodItem.Price * ci.Quantity);
        }
    }
}

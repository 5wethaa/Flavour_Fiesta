using Flavour_Fiesta.Data;
using Flavour_Fiesta.DataAccess.Interfaces;
using Flavour_Fiesta.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Flavour_Fiesta.DataAccess.Repositories
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly ApplicationDbContext _context;

        public FoodItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FoodItem>> GetFilteredItemsAsync(string? category, string? query)
        {
            var items = _context.FoodItems.AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                items = items.Where(x => x.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
                items = items.Where(x => x.Name.Contains(query) || x.Category.Contains(query));
            }

            return await items.ToListAsync();
        }

        public async Task<List<FoodItem>> GetAllAsync()
        {
            return await _context.FoodItems.ToListAsync();
        }

        public async Task<FoodItem?> GetByIdAsync(int id)
        {
            return await _context.FoodItems.FindAsync(id);
        }

        public async Task AddAsync(FoodItem item)
        {
            _context.FoodItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FoodItem item)
        {
            var existingItem = await _context.FoodItems.FindAsync(item.Id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Price = item.Price;
                existingItem.Quantity = item.Quantity;
                existingItem.Category = item.Category;
                existingItem.ImageUrl = item.ImageUrl;

                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(int id)
        {
            var item = await _context.FoodItems.FindAsync(id);
            if (item != null)
            {
                _context.FoodItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}

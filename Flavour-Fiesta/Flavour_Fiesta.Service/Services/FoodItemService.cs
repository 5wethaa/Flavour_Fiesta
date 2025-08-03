using Flavour_Fiesta.Domain.Models;
using Flavour_Fiesta.Domain.Interfaces;

namespace Flavour_Fiesta.Service.Services
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IFoodItemRepository _repository;

        public FoodItemService(IFoodItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FoodItem>> GetFilteredItemsAsync(string? category, string? query)
        {
            return await _repository.GetFilteredItemsAsync(category, query);
        }

        public async Task<List<FoodItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<FoodItem?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(FoodItem item)
        {
            await _repository.AddAsync(item);
        }

        public async Task UpdateAsync(FoodItem item)
        {
            await _repository.UpdateAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

﻿using Flavour_Fiesta.Domain.Models;

namespace Flavour_Fiesta.DataAccess.Interfaces
{
    public interface IFoodItemRepository
    {
        Task<List<FoodItem>> GetFilteredItemsAsync(string? category, string? query);
        Task<List<FoodItem>> GetAllAsync();
        Task<FoodItem?> GetByIdAsync(int id);
        Task AddAsync(FoodItem item);
        Task UpdateAsync(FoodItem item);
        Task DeleteAsync(int id);
    }
}

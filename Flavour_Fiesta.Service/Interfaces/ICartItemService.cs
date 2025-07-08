using Flavour_Fiesta.Domain.Models;

namespace Flavour_Fiesta.Service.Interfaces
{
    public interface ICartItemService
    {
        Task<List<CartItem>> GetCartItemsAsync(int customerId);
        Task AddOrUpdateAsync(int customerId, int foodItemId);
        Task RemoveAsync(int cartItemId);
        Task<decimal> CalculateTotalAsync(int customerId);
    }
}

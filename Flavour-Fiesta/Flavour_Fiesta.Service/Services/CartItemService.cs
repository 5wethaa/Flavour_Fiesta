using Flavour_Fiesta.Domain.Models;
using Flavour_Fiesta.Domain.Interfaces;

namespace Flavour_Fiesta.Service.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _repo;

        public CartItemService(ICartItemRepository repo)
        {
            _repo = repo;
        }

        public Task<List<CartItem>> GetCartItemsAsync(int customerId)
            => _repo.GetCartItemsAsync(customerId);

        public Task AddOrUpdateAsync(int customerId, int foodItemId)
            => _repo.AddOrUpdateAsync(customerId, foodItemId);

        public Task RemoveAsync(int cartItemId)
            => _repo.RemoveAsync(cartItemId);

        public Task<decimal> CalculateTotalAsync(int customerId)
            => _repo.CalculateTotalAsync(customerId);
    }
}

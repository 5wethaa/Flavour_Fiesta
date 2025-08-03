using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public void UpdateQuantity(int cartItemId, string operation)
        {
            var item = _repo.GetById(cartItemId);
            if (item != null)
            {
                if (operation == "increase")
                    item.Quantity++;
                else if (operation == "decrease" && item.Quantity > 1)
                    item.Quantity--;

                _repo.SaveChanges();
            }
        }

    }
}

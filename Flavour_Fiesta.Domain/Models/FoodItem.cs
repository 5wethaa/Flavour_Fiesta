using Flavour_Fiesta.Domain.Models;

namespace Flavour_Fiesta.Domain.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Quantity { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}

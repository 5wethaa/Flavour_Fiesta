namespace Flavour_Fiesta.Domain.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        // Foreign key to FoodItem
        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; } = null!;

        // Foreign key to Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int Quantity { get; set; }
    }
}

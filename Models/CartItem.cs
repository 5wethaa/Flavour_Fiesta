using System.ComponentModel.DataAnnotations.Schema;

namespace Flavour_Fiesta.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        // Food item FK
        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        // Customer FK
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        public int Quantity { get; set; }
    }
}

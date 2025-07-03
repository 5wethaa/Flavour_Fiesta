using System.ComponentModel.DataAnnotations;

namespace Flavour_Fiesta.Models
{
    public class FoodItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}

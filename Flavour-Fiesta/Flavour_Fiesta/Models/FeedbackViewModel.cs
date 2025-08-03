using System.ComponentModel.DataAnnotations;

namespace Flavour_Fiesta.Models
{
    public class FeedbackViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comments { get; set; } = string.Empty;
    }
}

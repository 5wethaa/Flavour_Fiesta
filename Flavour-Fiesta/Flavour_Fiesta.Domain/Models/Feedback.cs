namespace Flavour_Fiesta.Domain.Models
{
    public class Feedback
    {
        public int Id { get; set; }  // Primary key

        public string? Name { get; set; }

        public string? Email { get; set; }

        public int Rating { get; set; }  // 1 to 5

        public string Comments { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; }
    }
}

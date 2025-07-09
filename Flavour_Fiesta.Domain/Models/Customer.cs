namespace Flavour_Fiesta.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsConfirmed { get; set; }
    }
}






//using Flavour_Fiesta.Domain.Models;

//namespace Flavour_Fiesta.Domain.Models
//{
//    public class Customer
//    {
//        public int Id { get; set; }
//        public string Email { get; set; } = string.Empty;
//        public string Password { get; set; } = string.Empty;
//        public string ConfirmPassword { get; set; } = string.Empty;
//        public bool IsConfirmed { get; set; }
//    }
//}
using System.ComponentModel.DataAnnotations.Schema;

namespace Flavour_Fiesta.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        // This is the hashed password stored in the database
        public string PasswordHash { get; set; } = string.Empty;

        // These two fields are used only for form input and validation
        [NotMapped]
        public string Password { get; set; } = string.Empty;

        [NotMapped]
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool IsConfirmed { get; set; }
    }
}

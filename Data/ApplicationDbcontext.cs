using Flavour_Fiesta.Models;
using Microsoft.EntityFrameworkCore;


namespace Flavour_Fiesta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers{ get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        



    }
}


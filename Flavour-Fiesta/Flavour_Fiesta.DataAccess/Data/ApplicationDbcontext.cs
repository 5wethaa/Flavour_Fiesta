using Flavour_Fiesta.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Flavour_Fiesta.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers{ get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Customer Model
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                      .IsRequired();

                entity.Property(e => e.IsConfirmed)
                      .HasDefaultValue(false);

                // Not mapped in DB
                entity.Ignore(e => e.Password);
                entity.Ignore(e => e.ConfirmPassword);
            });
        

        // FoodItem Model
        modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Category)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Price)
                      .IsRequired();

                entity.Property(e => e.Quantity)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.ImageUrl)
                      .IsRequired()
                      .HasMaxLength(200);
            });

            // CartItem Model
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                // FK: FoodItem
                entity.HasOne(e => e.FoodItem)
                      .WithMany()
                      .HasForeignKey(e => e.FoodItemId)
                      .OnDelete(DeleteBehavior.Cascade);

                // FK: Customer
                entity.HasOne(e => e.Customer)
                      .WithMany()
                      .HasForeignKey(e => e.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Quantity)
                      .IsRequired(); 
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(100);
                entity.Property(f => f.Email).IsRequired();
                entity.Property(f => f.Rating).IsRequired();
                entity.Property(f => f.Comments)
                      .IsRequired()
                      .HasMaxLength(1000);
                entity.Property(f => f.SubmittedAt).IsRequired();
            });
           


        }


    }
}


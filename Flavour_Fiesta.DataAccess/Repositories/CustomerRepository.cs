using Flavour_Fiesta.Data;
using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace Flavour_Fiesta.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Customer? GetByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(c => c.Email == email);
        }

        public Customer? GetByEmailAndPassword(string email, string password)
        {
            var hashed = HashPassword(password);
            return _context.Customers.FirstOrDefault(c => c.Email == email && c.PasswordHash == hashed);
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

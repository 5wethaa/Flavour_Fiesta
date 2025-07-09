using Flavour_Fiesta.Domain.Models;
using Flavour_Fiesta.DataAccess.Interfaces;
using BCrypt.Net;
using Flavour_Fiesta.Domain.Interfaces;

namespace Flavour_Fiesta.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public bool Register(Customer customer, out string message)
        {
            var existing = _repository.GetByEmail(customer.Email);
            if (existing != null)
            {
                message = "Email already registered.";
                return false;
            }

            // ✅ Hash the password
            customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            customer.IsConfirmed = true;

            _repository.Add(customer);
            message = "Registered successfully!";
            return true;
        }

        public Customer? Login(string email, string password, out string message)
        {
            var user = _repository.GetByEmail(email); // 🔍 Get user by email only

            // ✅ Check if user exists and password matches
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) || !user.IsConfirmed)
            {
                message = "Invalid credentials or unconfirmed user.";
                return null;
            }

            message = "Login successful.";
            return user;
        }

        public Task<(bool IsSuccess, string Message)> RegisterAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<(Customer? User, string Message)> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}

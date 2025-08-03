
using Flavour_Fiesta.Domain.Models;
using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Service.Helpers;

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

            customer.PasswordHash = PasswordHelper.HashPassword(customer.Password);
            customer.IsConfirmed = true;

            _repository.Add(customer);
            message = "Registered successfully!";
            return true;
        }

        public Customer? Login(string email, string password, out string message)
        {
            var user = _repository.GetByEmail(email);
            if (user == null)
            {
                message = "User not found.";
                return null;
            }

            if (!PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                message = "Invalid password.";
                return null;
            }

            if (!user.IsConfirmed)
            {
                message = "Account not confirmed.";
                return null;
            }

            message = "Login successful.";
            return user;
        }
    }
}

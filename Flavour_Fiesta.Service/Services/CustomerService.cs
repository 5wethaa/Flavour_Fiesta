//using Flavour_Fiesta.Domain.Models;
//using Flavour_Fiesta.DataAccess.Interfaces;
//using Flavour_Fiesta.Service.Interfaces;

//namespace Flavour_Fiesta.Service.Services
//{
//    public class CustomerService : ICustomerService
//    {
//        private readonly ICustomerRepository _repository;

//        public CustomerService(ICustomerRepository repository)
//        {
//            _repository = repository;
//        }

//        public bool Register(Customer customer, out string message)
//        {
//            var existing = _repository.GetByEmail(customer.Email);
//            if (existing != null)
//            {
//                message = "Email already registered.";
//                return false;
//            }

//            customer.IsConfirmed = true;
//            _repository.Add(customer);
//            message = "Registered successfully!";
//            return true;
//        }

//        public Customer? Login(string email, string password, out string message)
//        {
//            var user = _repository.GetByEmailAndPassword(email, password);

//            if (user == null)
//            {
//                message = "Invalid credentials or unconfirmed user.";
//                return null;
//            }

//            message = "Login successful.";
//            return user;
//        }
//    }
//}
using BCrypt.Net;
using Flavour_Fiesta.Domain.Models;
using Flavour_Fiesta.DataAccess.Interfaces;
using Flavour_Fiesta.Service.Interfaces;

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
            if (!IsStrongPassword(customer.Password, out message))
                return false;

            var existing = _repository.GetByEmail(customer.Email);
            if (existing != null)
            {
                message = "Email already registered.";
                return false;
            }

            customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            customer.IsConfirmed = true;

            _repository.Add(customer);
            message = "Registered successfully!";
            return true;
        }

        public Customer? Login(string email, string password, out string message)
        {
            var user = _repository.GetByEmail(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                message = "Invalid credentials or unconfirmed user.";
                return null;
            }

            message = "Login successful.";
            return user;
        }

        private bool IsStrongPassword(string password, out string message)
        {
            if (password.Length < 8)
            {
                message = "Password must be at least 8 characters.";
                return false;
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
            {
                message = "Password must include uppercase, lowercase letters, and a number.";
                return false;
            }

            message = string.Empty;
            return true;
        }
    }
}

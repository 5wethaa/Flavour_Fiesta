using Flavour_Fiesta.Domain.Models;

namespace Flavour_Fiesta.Domain.Interfaces
{
    public interface ICustomerService
    {
        bool Register(Customer customer, out string message);
        Customer? Login(string email, string password, out string message);
    }
}

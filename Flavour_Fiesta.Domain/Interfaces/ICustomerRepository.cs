using Flavour_Fiesta.Domain.Models;

namespace Flavour_Fiesta.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Customer? GetByEmail(string email);
        Customer? GetByEmailAndPassword(string email, string password);
        void Add(Customer customer);
    }
}

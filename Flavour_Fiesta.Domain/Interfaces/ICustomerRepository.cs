using Flavour_Fiesta.Domain.Models;

namespace Flavour_Fiesta.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Customer? GetByEmail(string email);
        void Add(Customer customer);
       

    }
}

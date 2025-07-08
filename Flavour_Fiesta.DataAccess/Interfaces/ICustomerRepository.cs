//using Flavour_Fiesta.Domain.Models;

//namespace Flavour_Fiesta.DataAccess.Interfaces
//{
//    public interface ICustomerRepository
//    {
//        Customer? GetByEmail(string email);
//        Customer? GetByEmailAndPassword(string email, string password);
//        void Add(Customer customer);
//    }
//}
using Flavour_Fiesta.Domain.Models;

namespace Flavour_Fiesta.DataAccess.Interfaces
{
    public interface ICustomerRepository
    {
        Customer? GetByEmail(string email);
        void Add(Customer customer);
    }
}

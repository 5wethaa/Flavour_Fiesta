//using Flavour_Fiesta.Data;
//using Flavour_Fiesta.DataAccess.Interfaces;
//using Flavour_Fiesta.Domain.Models;

//namespace Flavour_Fiesta.DataAccess.Repositories
//{
//    public class CustomerRepository : ICustomerRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public CustomerRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public Customer? GetByEmail(string email)
//        {
//            return _context.Customers.FirstOrDefault(c => c.Email == email);
//        }

//        public Customer? GetByEmailAndPassword(string email, string password)
//        {
//            return _context.Customers
//                .FirstOrDefault(c => c.Email == email && c.Password == password && c.IsConfirmed);
//        }

//        public void Add(Customer customer)
//        {
//            _context.Customers.Add(customer);
//            _context.SaveChanges();
//        }
//    }
//}
using Flavour_Fiesta.Data;
using Flavour_Fiesta.DataAccess.Interfaces;
using Flavour_Fiesta.Domain.Models;

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

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
    }
}

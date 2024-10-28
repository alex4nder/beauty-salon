using BeautySalonApp.Data;
using BeautySalonApp.Models.BeautySalonApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class CustomerService
    {
        private DatabaseService _databaseService;
        private LocalDbContext _context;
        private readonly CurrentBranchContext _CurrentBranchContext;

        public CustomerService()
        {
            _CurrentBranchContext = Program.ServiceProvider.GetRequiredService<CurrentBranchContext>();
            _databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();

            _context = _databaseService.GetLocalDbContext(_CurrentBranchContext.BranchId);
        }

        public void CustomerAdd(Customer Customer)
        {
            _context.Customers.Add(Customer);

            _context.SaveChanges();
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(Guid CustomerId)
        {
            return _context.Customers.Find(CustomerId);
        }

        public void CustomerRemove(Guid CustomerId)
        {
            var Customer = _context.Customers.Find(CustomerId);
            if (Customer != null)
            {
                _context.Customers.Remove(Customer);
                _context.SaveChanges();
            }
        }

        public void CustomerEdit(Customer Customer)
        {
            var existingCustomer = _context.Customers.Find(Customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = Customer.FirstName;
                existingCustomer.LastName = Customer.LastName;
                existingCustomer.Phone = Customer.Phone;
                existingCustomer.Email = Customer.Email;
                existingCustomer.Birthday = Customer.Birthday;

                _context.SaveChanges();
            }
        }
    }
}

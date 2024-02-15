using BusinessObjects;

namespace Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();
        List<Customer> FindCustomersByName(string name);
        Customer? FindCustomerById(int id);
        Customer? FindCustomerByEmail(string email);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}

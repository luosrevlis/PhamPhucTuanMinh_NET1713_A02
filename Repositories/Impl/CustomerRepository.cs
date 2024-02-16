using BusinessObjects;
using BusinessObjects.Enums;
using DAOs;

namespace Repositories.Impl
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDAO _customerDao;
        private readonly ReservationDAO _reservationDao;

        public CustomerRepository(CustomerDAO customerDao, ReservationDAO reservationDao)
        {
            _customerDao = customerDao;
            _reservationDao = reservationDao;
        }

        public void AddCustomer(Customer customer)
        {
            _customerDao.Add(customer);
        }

        public void DeleteCustomer(int id)
        {
            var customer = _customerDao.GetById(id);
            if (customer == null)
            {
                return;
            }
            
            var reservations = _reservationDao.GetAll()
                .Where(res => res.CustomerId == id && res.BookingStatus != (byte)Status.Deleted)
                .ToList();
            foreach (var reservation in reservations)
            {
                _reservationDao.Delete(reservation);
            }

            customer.CustomerStatus = (byte)Status.Deleted;
            _customerDao.Update(customer);
        }

        public Customer? FindCustomerById(int id)
        {
            return _customerDao
                .GetFirst(customer => customer.CustomerId == id && customer.CustomerStatus != (byte)Status.Deleted);
        }

        public Customer? FindCustomerByEmail(string email)
        {
            return _customerDao.GetFirst(customer => customer.EmailAddress == email);
        }

        public List<Customer> FindCustomersByName(string name)
        {
            return _customerDao.GetAll()
                .Where(customer => (customer.CustomerFullName ?? string.Empty).Contains(name) && customer.CustomerStatus != (byte)Status.Deleted)
                .OrderBy(customer => customer.CustomerFullName)
                .ToList();
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerDao.GetAll()
                .Where(customer => customer.CustomerStatus != (byte)Status.Deleted)
                .OrderBy(customer => customer.CustomerFullName)
                .ToList();
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerDao.Update(customer);
        }
    }
}

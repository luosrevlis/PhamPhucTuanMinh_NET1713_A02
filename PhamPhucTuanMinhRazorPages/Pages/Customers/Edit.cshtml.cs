using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.Customers
{
    [Admin]
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;

        public EditModel(ICustomerRepository customerRepository, IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = _customerRepository.FindCustomerById((int)id);
            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(Customer.EmailAddress))
            {
                ModelState.AddModelError("Customer.EmailAddress", "Email cannot be empty!");
            }
            if (!CheckAdmin() || !CheckCustomer())
            {
                ModelState.AddModelError("Customer.EmailAddress", "Email has been registered!");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var customerToUpdate = _customerRepository.FindCustomerById((int)id);
            if (customerToUpdate == null)
            {
                return NotFound();
            }
            customerToUpdate.CustomerFullName = Customer.CustomerFullName;
            customerToUpdate.Telephone = Customer.Telephone;
            customerToUpdate.CustomerBirthday = Customer.CustomerBirthday;
            _customerRepository.UpdateCustomer(customerToUpdate);
            return RedirectToPage("Index");
        }

        private bool CheckAdmin()
        {
            string email = _configuration.GetSection("AdminAccount")["Username"] ?? string.Empty;
            if (email.Equals(Customer.EmailAddress))
            {
                return false;
            }
            return true;
        }

        private bool CheckCustomer()
        {
            var customer = _customerRepository.FindCustomerByEmail(Customer.EmailAddress);
            if (customer != null)
            {
                return false;
            }
            return true;
        }
    }
}

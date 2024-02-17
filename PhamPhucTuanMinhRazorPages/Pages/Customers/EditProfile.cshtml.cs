using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.Customers
{
    [Customer]
    public class EditProfileModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;

        public EditProfileModel(ICustomerRepository customerRepository, IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new();
        [BindProperty]
        public string RetypePassword { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            int id = HttpContext.Session.GetInt32(SessionConst.UserIdKey) ?? -1;
            var customer = _customerRepository.FindCustomerById((int)id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.Password = string.Empty;
            Customer = customer;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Customer.EmailAddress))
            {
                ModelState.AddModelError("Customer.EmailAddress", "Email cannot be empty!");
            }
            if (!RetypePassword.Equals(Customer.Password))
            {
                ModelState.AddModelError("RetypePassword", "Password does not match!");
            }
            if (!CheckAdmin() || !CheckCustomer())
            {
                ModelState.AddModelError("Customer.EmailAddress", "Email has been registered!");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int id = HttpContext.Session.GetInt32(SessionConst.UserIdKey) ?? -1;
            var customerToUpdate = _customerRepository.FindCustomerById((int)id);
            if (customerToUpdate == null)
            {
                return NotFound();
            }
            customerToUpdate.EmailAddress = Customer.EmailAddress;
            customerToUpdate.Password = Customer.Password;
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

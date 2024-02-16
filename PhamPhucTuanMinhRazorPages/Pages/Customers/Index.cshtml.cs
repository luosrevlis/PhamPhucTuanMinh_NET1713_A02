using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.Customers
{
    [Admin]
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public IndexModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IList<Customer> CustomerList { get; set; } = new List<Customer>();

        [BindProperty]
        public string CustomerName { get; set; } = string.Empty;

        public void OnGet()
        {
            CustomerList = _customerRepository.GetAllCustomers();
        }

        public void OnPost()
        {
            CustomerList = _customerRepository.FindCustomersByName(CustomerName ?? string.Empty);
        }
    }
}

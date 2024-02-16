using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.Customers
{
    [Admin]
    public class DetailsModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public DetailsModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

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
    }
}

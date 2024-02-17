using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Enums;

namespace PhamPhucTuanMinhRazorPages.Pages.BookingReservations
{
    [Authenticated]
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IActionResult OnGet()
        {
            int userRole = HttpContext.Session.GetInt32(SessionConst.UserRoleKey) ?? (int)UserRole.None;
            if (userRole == (int)UserRole.Admin)
            {
                ViewData["Customers"] = new SelectList(_customerRepository.GetAllCustomers(), "CustomerId", "EmailAddress");
            }
            else if (userRole == (int)UserRole.Customer)
            {
                CustomerId = HttpContext.Session.GetInt32(SessionConst.UserIdKey) ?? -1;
            }
            return Page();
        }

        [BindProperty]
        public int CustomerId { get; set; }
        [BindProperty]
        public DateTime StayStart { get; set; }
        [BindProperty]
        public DateTime StayEnd { get; set; }
        
        public IActionResult OnPost()
        {
            if (StayStart < DateTime.Now || StayEnd < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Staying period cannot be in the past!");
            }
            if (StayStart >= StayEnd)
            {
                ModelState.AddModelError(string.Empty, "The end date must be at least 1 day after the start date!");
            }
            if (!ModelState.IsValid)
            {
                ViewData["Customers"] = new SelectList(_customerRepository.GetAllCustomers(), "CustomerId", "EmailAddress");
                return Page();
            }
            return RedirectToPage("AvailableRooms", new { customerId = CustomerId, stayStart = StayStart, stayEnd = StayEnd });
        }
    }
}

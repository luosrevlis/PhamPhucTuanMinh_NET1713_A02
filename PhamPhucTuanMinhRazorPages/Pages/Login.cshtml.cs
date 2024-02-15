﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;
using PhamPhucTuanMinhRazorPages.Enums;
using PhamPhucTuanMinhRazorPages.Constants;

namespace PhamPhucTuanMinhRazorPages.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;

        public LoginModel(ICustomerRepository customerRepository, IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = new();

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Customer.EmailAddress) || string.IsNullOrEmpty(Customer.Password))
            {
                ModelState.AddModelError("EmptyUidPwd", "Email and password cannot be empty!");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (CheckAdmin())
            {
                HttpContext.Session.SetInt32(SessionConst.UserRoleKey, (int)UserRole.Admin);
                return RedirectToPage("AdminMenu");
            }
            else if (CheckCustomer())
            {
                HttpContext.Session.SetInt32(SessionConst.UserIdKey, Customer.CustomerId);
                HttpContext.Session.SetInt32(SessionConst.UserRoleKey, (int)UserRole.Customer);
                return RedirectToPage("CustomerMenu");
            }
            ModelState.AddModelError("NotFound", "Incorrect email or password!");
            return Page();
        }

        private bool CheckAdmin()
        {
            string email = _configuration.GetSection("AdminAccount")["Username"] ?? string.Empty;
            string password = _configuration.GetSection("AdminAccount")["Password"] ?? string.Empty;
            if (email.Equals(Customer.EmailAddress) && password.Equals(Customer.Password))
            {
                return true;
            }
            return false;
        }

        private bool CheckCustomer()
        {
            var customer = _customerRepository.FindCustomerByEmail(Customer.EmailAddress);
            if (customer == null)
            {
                return false;
            }
            string inputPassword = Customer.Password ?? string.Empty;
            if (inputPassword.Equals(customer.Password))
            {
                return true;
            }
            return false;
        }
    }
}

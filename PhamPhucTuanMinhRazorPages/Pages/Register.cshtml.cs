﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using BusinessObjects.Enums;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;

        public RegisterModel(ICustomerRepository customerRepository, IConfiguration configuration)
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
        [BindProperty]
        public string RetypePassword { get; set; } = string.Empty;

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Customer.EmailAddress) || string.IsNullOrEmpty(Customer.Password))
            {
                ModelState.AddModelError(string.Empty, "Email and password cannot be empty!");
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
            Customer cleanCustomer = new()
            {
                EmailAddress = Customer.EmailAddress,
                Password = Customer.Password,
                CustomerFullName = Customer.CustomerFullName,
                Telephone = Customer.Telephone,
                CustomerBirthday = Customer.CustomerBirthday,
                CustomerStatus = (byte)Status.NotDeleted
            };
            _customerRepository.AddCustomer(cleanCustomer);
            return RedirectToPage("Login");
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

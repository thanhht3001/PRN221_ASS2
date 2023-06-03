using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using HoTanThanhRazorPages.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Repository.CustomerRepo;

namespace HoTanThanhRazorPages.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepo repo = new CustomerRepo();

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public CreateModel() { }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var customer = new Customer
            {
                CustomerId = Customer.CustomerId,
                CustomerName = Customer.CustomerName,
                Email = Customer.Email,
                City = Customer.City,
                Country = Customer.Country,
                Password = Customer.Password,
                Birthday = Customer.Birthday
            };
            if (repo.Exist(customer.CustomerId))
            {
                ViewData["Message"] = "Customer ID already exist!";
                return Page();
            }
            else
            {
                repo.Save(customer);
                return RedirectToPage("/Index");
            }
        }
    }
}

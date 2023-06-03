using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using HoTanThanhRazorPages.ViewModels;
using System.Text;
using Repository.CustomerRepo;

namespace HoTanThanhRazorPages.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepo repo = new CustomerRepo();

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public EditModel() { }

        public IActionResult OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var customer = repo.GetCustomer(id);
            var customerViewmodel = new CustomerViewModel
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                Email = customer.Email,
                City = customer.City,
                Country = customer.Country,
                Password = customer.Password,
                Birthday = customer.Birthday
            };
            Customer = customerViewmodel;
            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                string param = $"/{Customer.CustomerId}";
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
                if (Customer != null)
                {
                    repo.Update(customer);
                    return RedirectToPage("./Index");
                }
                else
                {
                    return Page();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

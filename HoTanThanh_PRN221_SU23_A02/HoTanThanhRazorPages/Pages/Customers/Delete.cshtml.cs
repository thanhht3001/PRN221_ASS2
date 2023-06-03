using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using HoTanThanhRazorPages.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using Repository.CustomerRepo;

namespace HoTanThanhRazorPages.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerRepo repo = new CustomerRepo();
        
        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public DeleteModel() { }

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

        public IActionResult OnPostAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            else
            {
                var customer = repo.GetCustomer(id);
                repo.Delete(customer);
                return RedirectToPage("./Index");
            }   
        }
    }
}

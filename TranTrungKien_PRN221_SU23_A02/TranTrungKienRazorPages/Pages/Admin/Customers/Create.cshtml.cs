using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.PageModels;
using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;
using System;

namespace TranTrungKienRazorPages.Pages.Admin.Customers
{
    [Authorize(Roles = nameof(eRole.Admin))]
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

            if (repo.doEmailExist(Customer.Email))
            {
                ModelState.AddModelError("", "The email already exists!");
                return Page();
            }

            try
            {
                Random rand = new Random();
                int customerId = rand.Next(1, 10000);
                var customer = new Customer
                {
                    CustomerId = customerId,
                    CustomerName = Customer.CustomerName,
                    Email = Customer.Email,
                    City = Customer.City,
                    Country = Customer.Country,
                    Password = Customer.Password,
                    Birthday = Customer.Birthday
                };
                repo.Save(customer);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("", "Something went wrong, please try again later!");
                return Page();
            }

        }
    }
}

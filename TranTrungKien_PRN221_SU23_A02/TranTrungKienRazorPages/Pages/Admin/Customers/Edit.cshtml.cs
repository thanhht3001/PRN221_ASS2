using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System;

using TranTrungKienRazorPages.PageModels;
using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Admin.Customers
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class EditModel : PageModel
    {
        private readonly ICustomerRepo repo = new CustomerRepo();

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public EditModel() { }

        public IActionResult OnGetAsync(int id)
        {
            var customer = repo.GetCustomer(id);
            if (customer == null)
            {
                return RedirectToPage("./Index");
            }

            Customer = new CustomerViewModel
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                Email = customer.Email,
                City = customer.City,
                Country = customer.Country,
                Password = customer.Password,
                Birthday = customer.Birthday
            };
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
                var customer = new Customer
                {
                    CustomerId = (int)Customer.CustomerId,
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

                return Page();
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

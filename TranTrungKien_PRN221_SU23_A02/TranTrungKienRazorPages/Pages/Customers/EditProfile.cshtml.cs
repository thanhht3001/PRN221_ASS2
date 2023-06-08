using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using TranTrungKienRazorPages.PageModels;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Customers
{
    [Authorize(Roles = nameof(eRole.Customer))]
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public CustomerViewModel Customer { get; set; }
        private readonly ICustomerRepo repo = new CustomerRepo();

        public EditProfileModel() { }

        public IActionResult OnGetAsync()
        {
            var customer = repo.GetCustomer(HttpContext.getAccountId());

            var customerViewmodel = new CustomerViewModel
            {
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
            var customer = new Customer
            {
                CustomerName = Customer.CustomerName,
                Email = Customer.Email,
                City = Customer.City,
                Country = Customer.Country,
                Password = Customer.Password,
                Birthday = Customer.Birthday
            };
            repo.Update(customer);
            return RedirectToPage("./Profile");
        }
    }
}

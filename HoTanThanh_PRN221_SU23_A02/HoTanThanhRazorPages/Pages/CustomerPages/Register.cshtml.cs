using BusinessObject.Models;
using HoTanThanhRazorPages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Repository.CustomerRepo;

namespace HoTanThanhRazorPages.Pages.CustomerPages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        private readonly ICustomerRepo repo = new CustomerRepo();

        public RegisterModel() { }

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

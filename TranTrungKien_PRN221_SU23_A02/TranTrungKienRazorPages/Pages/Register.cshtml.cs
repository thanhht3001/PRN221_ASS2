using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System;

using TranTrungKienRazorPages.PageModels;
using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages
{
    public class RegisterModel : PageModel
    {
        public RegisterModel() { }

        [BindProperty]
        public RegisterViewModel User { get; set; }
        private readonly ICustomerRepo repo = new CustomerRepo();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (repo.doEmailExist(User.Email))
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
                    CustomerName = User.Name,
                    Email = User.Email,
                    City = User.City,
                    Country = User.Country,
                    Password = User.Password,
                    Birthday = User.Birthday
                };
                repo.Save(customer);

                var claims = new List<Claim>
                {
                    new(ClaimType.Role, eRole.Customer.ToString()),
                    new(ClaimType.AccountId, customer.CustomerId.ToString()),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToPage("/Customers/Index");
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

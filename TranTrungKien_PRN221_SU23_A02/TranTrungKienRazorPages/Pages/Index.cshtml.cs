using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

using TranTrungKienRazorPages.PageModels;
using DataAccess.Repositories;
using BusinessObject;
using BusinessObject.Models;
using TranTrungKienRazorPages.Utils;
using System;

namespace TranTrungKienRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepo repository = new CustomerRepo();
        private readonly AdminAccount adminAccount;

        public IndexModel(IOptions<AdminAccount> logger)
        {
            adminAccount = logger.Value;
        }

        [BindProperty]
        public LoginViewModel LoginModel { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var role = HttpContext.User.FindFirst(ClaimType.Role.ToString());
                return RedirectToPage(role.Value == eRole.Admin.ToString() ? "/Admin/Customers/Index" : "/Customers/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var email = LoginModel.Email;
            var password = LoginModel.Password;
            if (email == adminAccount.Email && password == adminAccount.Password)
            {
                var adminIdentity = new ClaimsIdentity(new List<Claim>
                {
                    new(ClaimType.Role, eRole.Admin.ToString()),
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var adminPrinciple = new ClaimsPrincipal(adminIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, adminPrinciple);
                return RedirectToPage("/Admin/Customers/Index");
            }

            try
            {
                Customer customer = repository.Login(email, password);
                if (customer is null)
                {
                    ModelState.AddModelError("", "Incorrect email or password");
                    return Page();
                }

                var claims = new List<Claim>
                    {
                        new(ClaimType.Role, eRole.Customer.ToString()),
                        new(ClaimType.AccountId, customer.CustomerId.ToString()),
                    };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToPage("/Customers/Index");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("", "Something went wrong, please try again later!");
                return Page();
            }
        }
    }
}

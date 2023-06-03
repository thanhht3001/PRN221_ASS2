using BusinessObject;
using BusinessObject.Models;
using HoTanThanhRazorPages.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository.CustomerRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoTanThanhRazorPages.Pages
{
    public class IndexModel : PageModel
    {                                                                       
        private readonly ICustomerRepo repository = new CustomerRepo();
        private readonly AdminAccount adminAccount;

        [BindProperty]
        public LoginViewModel LoginModel { get; set; }
        public string Message { get; set; }

        public IndexModel(IOptions<AdminAccount> logger)
        {
            adminAccount = logger.Value;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {

            Customer customer = repository.Login(LoginModel.Email, LoginModel.Password);
            if (LoginModel.Email == adminAccount.Email && LoginModel.Password == adminAccount.Password)
            {
                //Admin login thành công
                ViewData["Message"] = "Login success";
                return RedirectToPage("/Customers/Index");
            }
            else if (customer != null)
            {
                //Customer login thành công
                HttpContext.Session.SetInt32("id", customer.CustomerId);
                ViewData["Message"] = "Login success";
                return RedirectToPage("/CustomerPages/FlowerBouquets");
            }
            else
            {
                ViewData["Message"] = "Incorrect email or password";
            }
            return Page();
        }
    }
}

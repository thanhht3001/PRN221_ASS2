using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Customers
{
    [Authorize(Roles = nameof(eRole.Customer))]
    public class ProfileModel : PageModel
    {
        public Customer Customer { get; set; }
        private readonly ICustomerRepo customerRepo = new CustomerRepo();

        public ProfileModel() { }

        public IActionResult OnGetAsync()
        {
            Customer = customerRepo.GetCustomer(HttpContext.getAccountId());
            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

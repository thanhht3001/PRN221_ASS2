using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Customers
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class DetailsModel : PageModel
    {
        private readonly ICustomerRepo repo = new CustomerRepo();
        public Customer Customer { get; set; }

        public DetailsModel() { }

        public IActionResult OnGetAsync(int id)
        {
            Customer = repo.GetCustomer(id);
            if(Customer == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

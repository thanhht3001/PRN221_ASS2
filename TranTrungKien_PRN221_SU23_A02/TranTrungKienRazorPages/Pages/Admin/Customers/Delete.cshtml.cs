using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.PageModels;
using TranTrungKienRazorPages.Utils;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Admin.Customers
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class DeleteModel : PageModel
    {
        private readonly ICustomerRepo repo = new CustomerRepo();

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public DeleteModel() { }

        public IActionResult OnGetAsync(int id)
        {
            var Customer = repo.GetCustomer(id);
            if (Customer == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public IActionResult OnPostAsync(int id)
        {
            var customer = repo.GetCustomer(id);
            if (customer == null)
            {
                ModelState.AddModelError("", "Something went wrong, please try again later!");
                return Page();
            }

            repo.Delete(customer);
            return RedirectToPage("./Index");

        }
    }
}

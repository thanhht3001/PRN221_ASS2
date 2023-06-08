using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Orders
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class DeleteModel : PageModel
    {
        private readonly IOrderRepo repo = new OrderRepo();

        [BindProperty]
        public Order Order { get; set; }

        public DeleteModel() { }

        public IActionResult OnGetAsync(int id)
        {
            if(id != 0)
            {
                Order = repo.GetOrder(id);
                return Page();
            } else
            {
                return NotFound();
            }
        }

        public IActionResult OnPostAsync(int id)
        {
            if (id != 0)
            {
                Order = repo.GetOrder(id);
                repo.Delete(Order);
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }
    }
}

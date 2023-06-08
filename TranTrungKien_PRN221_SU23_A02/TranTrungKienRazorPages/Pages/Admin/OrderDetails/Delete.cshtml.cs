using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.OrderDetails
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class DeleteModel : PageModel
    {
        private readonly IOrderDetailRepo repo = new OrderDetailRepo();

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }
        [ViewData]
        public int OrderId { get; set; }

        public DeleteModel() { }

        public IActionResult OnGetAsync(int id, int id2)
        {
            if (id <= 0 || id2 <= 0)
            {
                return NotFound();
            }
            OrderId = id;
            OrderDetail = repo.SearchOrderDetailByOrderIdAndByFlowerBouquetId(id, id2);
            if (OrderDetail == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (OrderDetail != null)
            {
                repo.Delete(OrderDetail);
                return RedirectToPage("./Index", new { id = OrderDetail.OrderId });
            }
            else
            {
                return Page();
            }
        }
    }
}

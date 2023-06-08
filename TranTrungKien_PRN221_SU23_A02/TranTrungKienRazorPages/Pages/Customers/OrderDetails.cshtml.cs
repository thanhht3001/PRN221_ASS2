using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

using TranTrungKienRazorPages.Utils;
using DataAccess.Repositories;
using BusinessObject.Models;

namespace TranTrungKienRazorPages.Pages.Customers
{
    [Authorize(Roles = nameof(eRole.Customer))]
    public class OrderDetailsModel : PageModel
    {
        public IList<OrderDetail> OrderDetail { get; set; }
        private readonly IOrderDetailRepo repo = new OrderDetailRepo();

        public OrderDetailsModel() { }

        [ViewData]
        public int? OrderId { get; set; }

        public IActionResult OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            OrderDetail = repo.GetOrderDetailByOrderId(id);
            return Page();
        }
    }
}

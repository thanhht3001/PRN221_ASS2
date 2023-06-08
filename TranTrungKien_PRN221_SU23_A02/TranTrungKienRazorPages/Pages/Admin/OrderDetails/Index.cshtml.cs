using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.OrderDetails
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class IndexModel : PageModel
    {
        public IList<OrderDetail> OrderDetail { get;set; }
        private readonly IOrderDetailRepo repo = new OrderDetailRepo();
        private readonly IOrderRepo orderRepo = new OrderRepo();

        [ViewData]
        public int? OrderId { get; set; }

        public IndexModel() { }

        public IActionResult OnGetAsync(int id)
        {
            
            OrderDetail = repo.GetOrderDetailByOrderId(id);
            decimal total = 0;
            foreach (var item in OrderDetail)
            {
                total += (item.UnitPrice * item.Quantity) * (decimal)((100 - item.Discount) / 100);
            }
            OrderId = id;
            return Page();
        }
    }
}

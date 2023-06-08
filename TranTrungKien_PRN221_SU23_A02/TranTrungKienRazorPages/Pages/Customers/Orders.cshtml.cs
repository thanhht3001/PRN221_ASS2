using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

using TranTrungKienRazorPages.Utils;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace TranTrungKienRazorPages.Pages.Customers
{
    [Authorize(Roles = nameof(eRole.Customer))]
    public class OrdersModel : PageModel
    {
        public IList<Order> Order { get; set; }
        private readonly IOrderRepo repo = new OrderRepo();

        public OrdersModel() { }

        public IActionResult OnGetAsync()
        {
            Order = repo.GetOrderByCustomerId(HttpContext.getAccountId());
            return Page();
        }
    }
}

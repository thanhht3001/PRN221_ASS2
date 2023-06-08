using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Orders
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class IndexModel : PageModel
    {
        public IList<Order> Order { get;set; }
        private readonly IOrderRepo repo = new OrderRepo();

        public IndexModel() { }

        public IActionResult OnGetAsync()
        {
            Order = repo.GetOrders();
            return Page();
        }
    }
}

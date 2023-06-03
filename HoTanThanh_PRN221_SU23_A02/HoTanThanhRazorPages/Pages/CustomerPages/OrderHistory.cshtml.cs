using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Repository.OrderRepo;

namespace HoTanThanhRazorPages.Pages.CustomerPages
{
    public class OrderHistoryModel : PageModel
    {
        public IList<Order> Order { get; set; }
        private readonly IOrderRepo repo = new OrderRepo();

        public OrderHistoryModel() { }

        public IActionResult OnGetAsync(int id)
        {
            Order = repo.GetOrderByCustomerId(id);
            return Page();
        }
    }
}

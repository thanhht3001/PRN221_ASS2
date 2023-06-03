using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Repository.OrderDetailRepo;

namespace HoTanThanhRazorPages.Pages.CustomerPages
{
    public class OrderHistoryDetailsModel : PageModel
    {
        public IList<OrderDetail> OrderDetail { get; set; }
        private readonly IOrderDetailRepo repo = new OrderDetailRepo();

        [ViewData]
        public int? OrderId { get; set; }

        public OrderHistoryDetailsModel() { }

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

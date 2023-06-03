using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Repository.OrderDetailRepo;
using Repository.OrderRepo;

namespace HoTanThanhRazorPages.Pages.OrderDetails
{
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
            Order order = orderRepo.GetOrder(id);
            var newOrder = new Order
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                ShippedDate = order.ShippedDate,
                CustomerId = order.CustomerId,
                Total = total
            };
            return Page();
        }
    }
}

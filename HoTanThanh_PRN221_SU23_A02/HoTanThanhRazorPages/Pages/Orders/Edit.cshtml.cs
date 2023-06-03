using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http;
using HoTanThanhRazorPages.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;
using Repository.OrderRepo;
using Repository.CustomerRepo;

namespace HoTanThanhRazorPages.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly IOrderRepo repo = new OrderRepo();
        private readonly ICustomerRepo customerRepo = new CustomerRepo();

        [BindProperty]
        public OrderViewModel Order { get; set; }

        public EditModel() { }

        public IActionResult OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var order = repo.GetOrder(id);
            var orderViewModel = new OrderViewModel
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                ShippedDate = order.ShippedDate,
                CustomerId = order.CustomerId,
                Total = order.Total
            };
            Order = orderViewModel;
            if (Order == null)
            {
                return NotFound();
            }
            var customers = customerRepo.GetCustomers();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerName");
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var customers = customerRepo.GetCustomers();
                ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerName");
                return Page();
            }
            try
            {
                string param = $"/{Order.OrderId}";
                var order = new Order
                {
                    OrderId = Order.OrderId,
                    OrderDate = Order.OrderDate,
                    OrderStatus = Order.OrderStatus,
                    ShippedDate = Order.ShippedDate,
                    CustomerId = Order.CustomerId,
                    Total = Order.Total
                };
                if (order != null)
                {
                    repo.Update(order);
                    return RedirectToPage("/Orders/Index");
                }
                else
                {
                    return Page();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

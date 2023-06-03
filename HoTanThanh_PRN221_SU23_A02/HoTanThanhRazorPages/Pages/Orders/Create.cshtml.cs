using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using HoTanThanhRazorPages.ViewModels;
using System.Net.Http.Headers;
using System.Text;
using Repository.OrderRepo;
using Repository.CustomerRepo;

namespace HoTanThanhRazorPages.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IOrderRepo repo = new OrderRepo();
        private readonly ICustomerRepo customerRepo = new CustomerRepo();

        [BindProperty]
        public OrderViewModel Order { get; set; }

        public CreateModel() {}

        public IActionResult OnGet()
        {
            var customers = customerRepo.GetCustomers();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerName");
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            var customers = customerRepo.GetCustomers();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerName");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var order = new Order
            {
                OrderId = Order.OrderId,
                OrderDate = Order.OrderDate,
                OrderStatus = Order.OrderStatus,
                ShippedDate = Order.ShippedDate,
                CustomerId = Order.CustomerId,
                Total = Order.Total
            };
            if (repo.Exist(order.OrderId))
            {
                ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerName");
                ViewData["Message"] = "Order ID already exist!";
                return Page();
            }
            else
            {
                repo.Save(order);
                return RedirectToPage("./Index");
            }
        }
    }
}

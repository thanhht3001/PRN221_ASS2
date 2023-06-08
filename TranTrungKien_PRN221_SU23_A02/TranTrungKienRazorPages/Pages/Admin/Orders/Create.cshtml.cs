using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.PageModels;
using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;
using System;

namespace TranTrungKienRazorPages.Pages.Orders
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class CreateModel : PageModel
    {
        private readonly IOrderRepo repo = new OrderRepo();
        private readonly ICustomerRepo customerRepo = new CustomerRepo();

        public CreateModel() { }

        [BindProperty]
        public OrderViewModel Order { get; set; }

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

            try
            {
                Random rand = new Random();
                int orderId = rand.Next(1, 10000);
                var order = new Order
                {
                    OrderId = orderId,
                    OrderDate = Order.OrderDate,
                    OrderStatus = Order.OrderStatus,
                    ShippedDate = Order.ShippedDate,
                    CustomerId = Order.CustomerId,
                    Total = Order.Total
                };
                repo.Save(order);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("", "Something went wrong, please try again later!");
                return Page();
            }
        }
    }
}

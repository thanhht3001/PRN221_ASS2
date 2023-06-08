using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System;

using TranTrungKienRazorPages.PageModels;
using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Orders
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class EditModel : PageModel
    {
        private readonly IOrderRepo repo = new OrderRepo();
        private readonly ICustomerRepo customerRepo = new CustomerRepo();

        [BindProperty]
        public OrderViewModel Order { get; set; }

        public EditModel() { }

        public IActionResult OnGetAsync(int id)
        {
            var order = repo.GetOrder(id);
            if (order == null)
            {
                return RedirectToPage("../Orders/Index");
            }

            Order = new OrderViewModel
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                ShippedDate = order.ShippedDate,
                CustomerId = order.CustomerId,
                Total = order.Total
            };
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
                var order = new Order
                {
                    OrderId = (int)Order.OrderId,
                    OrderDate = Order.OrderDate,
                    OrderStatus = Order.OrderStatus,
                    ShippedDate = Order.ShippedDate,
                    CustomerId = Order.CustomerId,
                    Total = Order.Total
                };
                if (order != null)
                {
                    repo.Update(order);
                    return RedirectToPage("../Orders/Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("", "Something went wrong, please try again later!");

                var customers = customerRepo.GetCustomers();
                ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerName");
                return Page();
            }
        }
    }
}

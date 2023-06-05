using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Repository.FlowerBouquetRepo;
using Repository.OrderDetailRepo;
using Repository.OrderRepo;

namespace HoTanThanhRazorPages.Pages.FlowerBouquets
{
    public class DeleteModel : PageModel
    {
        private readonly IFlowerRepo repo = new FlowerRepo();
        private readonly IOrderDetailRepo orderDetailRepo = new OrderDetailRepo();
        private readonly IOrderRepo orderRepo = new OrderRepo();

        [BindProperty]
        public FlowerBouquet FlowerBouquet { get; set; }

        public DeleteModel() { }

        public IActionResult OnGetAsync(int id)
        {
            FlowerBouquet = repo.GetFlower(id);
            if (FlowerBouquet == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostAsync(int id)
        {
            List<Order> order = (List<Order>)orderRepo.GetOrders();
            List<int> listId = new List<int>();

            FlowerBouquet = repo.GetFlower(id);

            bool check = true;

            foreach (Order orderItem in order)
            {
                listId.Add(orderItem.OrderId);
            }
            foreach (var orderId in listId)
            {
                List<OrderDetail> orderdetail = (List<OrderDetail>)orderDetailRepo.GetOrderDetailByOrderId(orderId);
                foreach (var orderDetail in orderdetail)
                {
                    if (orderDetail.FlowerBouquetId == id)
                    {
                        check = false; break;
                    }
                }
                if (check == false)
                {
                    break;
                }
            }

            if (!check)
            {
                ViewData["Message"] = "FlowerBouquet are in cart! Cannot delete!";
                return Page();
            }
            else
            {
                
                repo.Delete(FlowerBouquet);
                return RedirectToPage("./Index");
            }

        }
    }
}

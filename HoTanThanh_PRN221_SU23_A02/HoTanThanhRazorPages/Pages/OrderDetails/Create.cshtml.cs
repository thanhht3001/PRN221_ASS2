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
using System.Net.Http.Headers;
using HoTanThanhRazorPages.ViewModels;
using System.Text;
using Repository.OrderDetailRepo;
using Repository.FlowerBouquetRepo;

namespace HoTanThanhRazorPages.Pages.OrderDetails
{
    public class CreateModel : PageModel
    {
        private readonly IOrderDetailRepo repo = new OrderDetailRepo();
        private readonly IFlowerRepo flowerRepo = new FlowerRepo();
        [BindProperty]
        public OrderDetailViewModel OrderDetail { get; set; }

        [ViewData]
        public int? OrderId { get; set; }

        [ViewData]
        public string Message { get; set; }

        IList<FlowerBouquet> FlowerBouquets { get; set; }

        public CreateModel() { }

        public IActionResult OnGet(int? id)
        {
            OrderId = id;
            FlowerBouquets = flowerRepo.GetFlowers();
            ViewData["FlowerBouquetId"] = new SelectList(FlowerBouquets, "FlowerBouquetId", "FlowerBouquetName");
            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            try
            {
                OrderId = id;
                FlowerBouquets = flowerRepo.GetFlowers();
                if (!ModelState.IsValid)
                {
                    ViewData["FlowerBouquetId"] = new SelectList(FlowerBouquets, "FlowerBouquetId", "FlowerBouquetName");
                    return Page();
                }
                var orderDetail = new OrderDetail
                {
                    OrderId = (int)id,
                    FlowerBouquetId = OrderDetail.FlowerBouquetId,
                    Quantity = OrderDetail.Quantity,
                    Discount = OrderDetail.Discount,
                    UnitPrice = OrderDetail.UnitPrice
                };
                repo.Save(orderDetail);
                return RedirectToPage("./Index", new { id = OrderId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Message = "This flower bouquet is already exist! Go to edit!";
                ViewData["FlowerBouquetId"] = new SelectList(FlowerBouquets, "FlowerBouquetId", "FlowerBouquetName");
                return Page();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

using TranTrungKienRazorPages.PageModels;
using TranTrungKienRazorPages.Utils;
using DataAccess.Repositories;
using BusinessObject.Models;

namespace TranTrungKienRazorPages.Pages.OrderDetails
{
    [Authorize(Roles = nameof(eRole.Admin))]
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

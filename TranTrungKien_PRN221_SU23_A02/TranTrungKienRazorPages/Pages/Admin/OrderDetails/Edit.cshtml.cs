using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TranTrungKienRazorPages.PageModels;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.OrderDetails
{
    public class EditModel : PageModel
    {
        private readonly IOrderDetailRepo repo = new OrderDetailRepo();
        private readonly IOrderRepo orderRepo = new OrderRepo();

        [BindProperty]
        public OrderDetailViewModel OrderDetail { get; set; }
        [ViewData]
        public int? OrderId { get; set; }

        public EditModel() { }

        public IActionResult OnGetAsync(int id, int id2)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            OrderId = id;

            var orderDetail = repo.SearchOrderDetailByOrderIdAndByFlowerBouquetId(id, id2);
            var orderDetailViewModel = new OrderDetailViewModel
            {
                OrderId = orderDetail.OrderId,
                FlowerBouquetId = orderDetail.FlowerBouquetId,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice,
                Discount = orderDetail.Discount
            };
            OrderDetail = orderDetailViewModel;
            if (OrderDetail == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            OrderId = OrderDetail.OrderId;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var orderDetail = new OrderDetail
            {
                OrderId = OrderDetail.OrderId,
                FlowerBouquetId = OrderDetail.FlowerBouquetId,
                Quantity = OrderDetail.Quantity,
                Discount = OrderDetail.Discount,
                UnitPrice = OrderDetail.UnitPrice
            };

            if (orderDetail != null)
            {
                repo.Update(orderDetail);
                return RedirectToPage("./Index", new { id = OrderDetail.OrderId });
            }
            else
            {
                return Page();
            }
        }

    }
}

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
using Repository.OrderDetailRepo;

namespace HoTanThanhRazorPages.Pages.OrderDetails
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderDetailRepo repo = new OrderDetailRepo();

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }
        [ViewData]
        public int OrderId { get; set; }

        public DeleteModel() { }

        public IActionResult OnGetAsync(int id, int id2)
        {
            if (id <= 0 || id2 <= 0)
            {
                return NotFound();
            }
            OrderId = id;
            OrderDetail = repo.SearchOrderDetailByOrderIdAndByFlowerBouquetId(id, id2);
            if (OrderDetail == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (OrderDetail != null)
            {
                repo.Delete(OrderDetail);
                return RedirectToPage("./Index", new { id = OrderDetail.OrderId });
            }
            else
            {
                return Page();
            }
        }
    }
}

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
using Repository.OrderRepo;

namespace HoTanThanhRazorPages.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderRepo repo = new OrderRepo();

        [BindProperty]
        public Order Order { get; set; }

        public DeleteModel() { }

        public IActionResult OnGetAsync(int id)
        {
            if(id != 0)
            {
                Order = repo.GetOrder(id);
                return Page();
            } else
            {
                return NotFound();
            }
        }

        public IActionResult OnPostAsync(int id)
        {
            if (id != 0)
            {
                Order = repo.GetOrder(id);
                repo.Delete(Order);
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }
    }
}

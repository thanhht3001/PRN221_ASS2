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

namespace HoTanThanhRazorPages.Pages.FlowerBouquets
{
    public class DeleteModel : PageModel
    {
        private readonly IFlowerRepo repo = new FlowerRepo();

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
            if (id <= 0)
            {
                return NotFound();
            } else
            {
                FlowerBouquet = repo.GetFlower(id);
                repo.Delete(FlowerBouquet);
                return RedirectToPage("./Index");
            }
        }
    }
}

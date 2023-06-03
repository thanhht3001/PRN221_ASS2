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
using Repository.FlowerBouquetRepo;

namespace HoTanThanhRazorPages.Pages.FlowerBouquets
{
    public class DetailsModel : PageModel
    {
        private readonly IFlowerRepo repo = new FlowerRepo();
        public FlowerBouquet FlowerBouquet { get; set; }

        public DetailsModel() { }

        public IActionResult OnGetAsync(int id)
        {
            FlowerBouquet = repo.GetFlower(id);
            if (FlowerBouquet == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

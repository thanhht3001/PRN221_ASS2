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
    public class IndexModel : PageModel
    {
        public IList<FlowerBouquet> FlowerBouquet { get;set; }
        private readonly IFlowerRepo repo = new FlowerRepo();

        [BindProperty]
        public string SearchString { get; set; }

        public IndexModel() { }

        public IActionResult OnGetAsync()
        {
            FlowerBouquet = repo.GetFlowers();
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            FlowerBouquet = repo.SearchByName(SearchString);
            return Page();
        }
    }
}

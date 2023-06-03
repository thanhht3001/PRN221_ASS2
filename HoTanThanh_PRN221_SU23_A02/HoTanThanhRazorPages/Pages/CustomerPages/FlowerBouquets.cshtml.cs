using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Repository.FlowerBouquetRepo;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace HoTanThanhRazorPages.Pages.CustomerPages
{
    public class FlowerBouquetsModel : PageModel
    {
        public IList<FlowerBouquet> FlowerBouquet { get; set; }
        private readonly IFlowerRepo repo = new FlowerRepo();

        [BindProperty]
        public string SearchString { get; set; }
        public FlowerBouquetsModel() { }

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

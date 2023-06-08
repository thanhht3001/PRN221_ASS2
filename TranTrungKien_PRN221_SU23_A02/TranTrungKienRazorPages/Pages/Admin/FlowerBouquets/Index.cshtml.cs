using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.FlowerBouquets
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class IndexModel : PageModel
    {
        public IList<FlowerBouquet> FlowerBouquet { get;set; }
        private readonly IFlowerRepo repo = new FlowerRepo();

        public IndexModel() { }

        [BindProperty]
        public string SearchString { get; set; }

        public IActionResult OnGetAsync()
        {
            FlowerBouquet = repo.GetFlowers();
            if(FlowerBouquet == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            FlowerBouquet = repo.SearchByName(SearchString);
            return Page();
        }
    }
}

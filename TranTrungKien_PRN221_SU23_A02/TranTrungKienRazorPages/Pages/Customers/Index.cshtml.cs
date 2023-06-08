using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Customers
{
    [Authorize(Roles = nameof(eRole.Customer))]
    public class IndexModel : PageModel
    {
        public IList<FlowerBouquet> FlowerBouquet { get; set; }
        private readonly IFlowerRepo repo = new FlowerRepo();

        public IndexModel() { }

        [BindProperty]
        public string SearchString { get; set; }

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

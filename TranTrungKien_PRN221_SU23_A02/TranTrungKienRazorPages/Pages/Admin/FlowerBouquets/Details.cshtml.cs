using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.FlowerBouquets
{
    [Authorize(Roles = nameof(eRole.Admin))]
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
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

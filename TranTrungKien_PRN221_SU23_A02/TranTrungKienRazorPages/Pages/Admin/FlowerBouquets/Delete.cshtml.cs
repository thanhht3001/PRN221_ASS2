using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.FlowerBouquets
{
    [Authorize(Roles = nameof(eRole.Admin))]
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
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public IActionResult OnPostAsync(int id)
        {
            var flowerBouquet = repo.GetFlower(id);
            if (flowerBouquet == null)
            {
                ModelState.AddModelError("", "Something went wrong, please try again later!");
                return Page();
            }

            repo.Delete(flowerBouquet);
            return RedirectToPage("./Index");
        }
    }
}

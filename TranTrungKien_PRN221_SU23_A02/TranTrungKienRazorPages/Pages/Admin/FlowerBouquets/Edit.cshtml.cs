using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

using TranTrungKienRazorPages.PageModels;
using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.FlowerBouquets
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class EditModel : PageModel
    {
        private readonly IFlowerRepo repo = new FlowerRepo();
        private readonly ICategoryRepo categoryRepo = new CategoryRepo();
        private readonly ISupplierRepo supplierRepo = new SupplierRepo();

        public IList<Category> Category { get; set; }
        public IList<Supplier> Supplier { get; set; }

        public EditModel() { }

        [BindProperty]
        public FlowerBouquetViewModel FlowerBouquet { get; set; }


        public IActionResult OnGetAsync(int id)
        {
            var FlowerBouquet = repo.GetFlower(id);
            if (FlowerBouquet == null)
            {
                return RedirectToPage("./Index");
            }

            Category = categoryRepo.GetCategories();
            Supplier = supplierRepo.GetSuppliers();
            ViewData["CategoryId"] = new SelectList(Category, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(Supplier, "SupplierId", "SupplierName");
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var flowerBouquet = new FlowerBouquet
                {
                    FlowerBouquetId = (int)FlowerBouquet.FlowerBouquetId,
                    FlowerBouquetName = FlowerBouquet.FlowerBouquetName,
                    CategoryId = FlowerBouquet.CategoryId,
                    Description = FlowerBouquet.Description,
                    UnitPrice = FlowerBouquet.UnitPrice,
                    UnitsInStock = FlowerBouquet.UnitsInStock,
                    FlowerBouquetStatus = FlowerBouquet.FlowerBouquetStatus,
                    SupplierId = FlowerBouquet.SupplierId
                };
                if (FlowerBouquet != null)
                {
                    repo.Update(flowerBouquet);
                    return RedirectToPage("./Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("", "Something went wrong, please try again later!");
                return Page();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

using TranTrungKienRazorPages.PageModels;
using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;
using System;

namespace TranTrungKienRazorPages.Pages.FlowerBouquets
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class CreateModel : PageModel
    {
        private readonly IFlowerRepo repo = new FlowerRepo();
        private readonly ICategoryRepo categoryRepo = new CategoryRepo();
        private readonly ISupplierRepo supplierRepo = new SupplierRepo();

        public CreateModel() { }

        [BindProperty]
        public FlowerBouquetViewModel FlowerBouquet { get; set; }
        public IList<Category> Category { get; set; }
        public IList<Supplier> Supplier { get; set; }

        public IActionResult OnGet()
        {
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
                Category = categoryRepo.GetCategories();
                Supplier = supplierRepo.GetSuppliers();
                ViewData["CategoryId"] = new SelectList(Category, "CategoryId", "CategoryName");
                ViewData["SupplierId"] = new SelectList(Supplier, "SupplierId", "SupplierName");
                return Page();
            }

            try
            {
                Random rand = new Random();
                int flowerBouquetId = rand.Next(1, 10000);
                var flowerBouquet = new FlowerBouquet
                {
                    FlowerBouquetId = flowerBouquetId,
                    FlowerBouquetName = FlowerBouquet.FlowerBouquetName,
                    Description = FlowerBouquet.Description,
                    UnitPrice = FlowerBouquet.UnitPrice,
                    UnitsInStock = FlowerBouquet.UnitsInStock,
                    CategoryId = FlowerBouquet.CategoryId,
                    SupplierId = FlowerBouquet.SupplierId,
                    FlowerBouquetStatus = FlowerBouquet.FlowerBouquetStatus,
                };
                repo.Save(flowerBouquet);
                return RedirectToPage("./Index");
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

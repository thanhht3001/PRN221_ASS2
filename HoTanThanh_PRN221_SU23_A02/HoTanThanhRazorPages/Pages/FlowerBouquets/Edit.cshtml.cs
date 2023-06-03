using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http;
using HoTanThanhRazorPages.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Repository.FlowerBouquetRepo;
using Repository.CategoryRepo;
using Repository.SupplierRepo;

namespace HoTanThanhRazorPages.Pages.FlowerBouquets
{
    public class EditModel : PageModel
    {
        private readonly IFlowerRepo repo = new FlowerRepo();
        private readonly ICategoryRepo categoryRepo = new CategoryRepo();
        private readonly ISupplierRepo supplierRepo = new SupplierRepo();

        public IList<Category> Category { get; set; }
        public IList<Supplier> Supplier { get; set; }

        [BindProperty]
        public FlowerBouquetViewModel FlowerBouquet { get; set; }

        public EditModel() { }

        public IActionResult OnGetAsync(int id)
        {
            Category = categoryRepo.GetCategories();
            Supplier = supplierRepo.GetSuppliers();
            ViewData["CategoryId"] = new SelectList(Category, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(Supplier, "SupplierId", "SupplierName");

            var flowerBouquet = repo.GetFlower(id);
            var flowerBouquetViewModel = new FlowerBouquetViewModel
            {
                FlowerBouquetId = flowerBouquet.FlowerBouquetId,
                FlowerBouquetName = flowerBouquet.FlowerBouquetName,
                CategoryId = flowerBouquet.CategoryId,
                Description = flowerBouquet.Description,
                UnitPrice = flowerBouquet.UnitPrice,
                UnitsInStock = flowerBouquet.UnitsInStock,
                FlowerBouquetStatus = flowerBouquet.FlowerBouquetStatus,
                SupplierId = flowerBouquet.SupplierId
            };
            FlowerBouquet = flowerBouquetViewModel;
            if (FlowerBouquet == null)
            {
                return NotFound();
            }
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
                string param = $"/{FlowerBouquet.FlowerBouquetId}";
                var flowerBouquet = new FlowerBouquet
                {
                    FlowerBouquetId = FlowerBouquet.FlowerBouquetId,
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
                } else
                {
                    return Page();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

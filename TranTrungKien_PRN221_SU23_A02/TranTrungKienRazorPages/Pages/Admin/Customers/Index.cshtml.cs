using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using TranTrungKienRazorPages.Utils;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace TranTrungKienRazorPages.Pages.Admin.Customers
{
    [Authorize(Roles = nameof(eRole.Admin))]
    public class IndexModel : PageModel
    {
        public IList<Customer> Customer { get;set; }
        private readonly ICustomerRepo repo = new CustomerRepo();

        public IndexModel() { }

        [BindProperty]
        public string SearchString { get; set; }

        public IActionResult OnGetAsync()
        {
            Customer = repo.GetCustomers();
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            Customer = repo.SearchByName(SearchString);
            return Page();
        }
    }
}

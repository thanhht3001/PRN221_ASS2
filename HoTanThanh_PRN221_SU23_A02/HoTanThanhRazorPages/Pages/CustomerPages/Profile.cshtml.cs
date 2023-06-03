using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Repository.CustomerRepo;

namespace HoTanThanhRazorPages.Pages.CustomerPages
{
    public class ProfileModel : PageModel
    {
        public Customer Customer { get; set; }
        private readonly ICustomerRepo customerRepo = new CustomerRepo();

        public ProfileModel() { }

        public IActionResult OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            
            Customer = customerRepo.GetCustomer(id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

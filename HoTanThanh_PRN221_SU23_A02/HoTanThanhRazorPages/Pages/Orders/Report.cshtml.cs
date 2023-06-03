using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Repository.OrderRepo;
using System;

namespace HoTanThanhRazorPages.Pages.Orders
{
    public class ReportModel : PageModel
    {
        public IList<Order> Order { get; set; }
        private readonly IOrderRepo repo = new OrderRepo();

        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }

        public int Number { get; set; }
        public decimal? Total { get; set; }

        public ReportModel() { }

        public IActionResult OnGetAsync()
        {
            Number = 0; Total = 0;
            Order = new List<Order>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            Order = repo.GetOrdersForReport(StartDate, EndDate);
            Number = Order.Count;
            Total = 0;
            if (Order.Count > 0)
            {
                foreach (var item in Order)
                {
                    Total += item.Total;
                }
            }
            return Page();
        }
    }
}

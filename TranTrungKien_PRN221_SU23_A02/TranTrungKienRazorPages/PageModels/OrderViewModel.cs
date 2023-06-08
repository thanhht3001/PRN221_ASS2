using System;
using System.ComponentModel.DataAnnotations;

namespace TranTrungKienRazorPages.PageModels
{
    public class OrderViewModel
    {
        public int? OrderId { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }

        [Required]
        [Display(Name = "Total")]
        public decimal? Total { get; set; }

        [Required]
        [Display(Name = "Order Status")]
        [MaxLength(10)]
        public string OrderStatus { get; set; }
    }
}

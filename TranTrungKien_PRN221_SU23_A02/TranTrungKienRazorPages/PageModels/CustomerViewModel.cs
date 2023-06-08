using System;
using System.ComponentModel.DataAnnotations;

namespace TranTrungKienRazorPages.PageModels
{
    public class CustomerViewModel
    {
        public int? CustomerId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(15)]
        public string City { get; set; }

        [Required]
        [StringLength(15)]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15)]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Birthday { get; set; }
    }
}

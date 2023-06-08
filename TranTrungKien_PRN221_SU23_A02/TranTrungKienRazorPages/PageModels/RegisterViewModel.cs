using System.ComponentModel.DataAnnotations;
using System;

namespace TranTrungKienRazorPages.PageModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

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

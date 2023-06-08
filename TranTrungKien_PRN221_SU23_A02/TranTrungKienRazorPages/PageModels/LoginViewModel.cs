﻿using System.ComponentModel.DataAnnotations;

namespace TranTrungKienRazorPages.PageModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

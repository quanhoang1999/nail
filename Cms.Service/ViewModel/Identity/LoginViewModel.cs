using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Service.ViewModel.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }
    }
}

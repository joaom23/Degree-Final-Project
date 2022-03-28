using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_API.Models.ViewModels
{
    public class RecuperarPasswordViewModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmNewPassword { get; set; }
    }
}

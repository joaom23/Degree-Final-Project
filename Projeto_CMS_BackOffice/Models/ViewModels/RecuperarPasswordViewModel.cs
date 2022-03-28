using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice.Models.ViewModels
{
    public class RecuperarPasswordViewModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength =5)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,15}$", ErrorMessage = "Password deve conter pelo menos um número, uma letra maiúscula e um caráter especial")]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,15}$", ErrorMessage = "Password deve conter pelo menos um número, uma letra maiúscula e um caráter especial")]
        public string ConfirmNewPassword { get; set; }

    }
}

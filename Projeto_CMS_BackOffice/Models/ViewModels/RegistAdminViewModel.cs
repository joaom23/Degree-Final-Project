using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice
{
    public class RegistAdminViewmodel
    {

        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,15}$", ErrorMessage = "Password deve conter pelo menos um número, uma letra maiúscula e um caráter especial")]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,15}$", ErrorMessage = "Password deve conter pelo menos um número, uma letra maiúscula e um caráter especial")]
        public string ConfirmPassword { get; set; }
    }
}

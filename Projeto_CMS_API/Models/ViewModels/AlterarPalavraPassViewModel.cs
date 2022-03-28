using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_API.Models.ViewModels
{
    public class AlterarPalavraPassViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string AntigaPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string NovaPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
    }
}

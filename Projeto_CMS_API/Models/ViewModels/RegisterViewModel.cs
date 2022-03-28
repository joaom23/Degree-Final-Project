using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_API.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [Required]
        //[StringLength(9, MinimumLength = 9)]
        public int Telefone { get; set; }

        //[Required]
        public string Foto { get; set; }

        public IFormFile FotoFile { get; set; }

    }
}

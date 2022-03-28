using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice
{
    public class Suspensões
    {
        [Required]
        public DateTime? DataBloqueio { get; set; }

        [Required]
        [StringLength(50)]
        public string Motivo { get; set; }

        [Key]
        [Required]
        public int IdU { get; set; }

        public IdentityUser Utiizador {get; set;} 

        [Required]
        public int IdAdm { get; set; }

        public Administrador Administrador { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice
{
    public class Administrador
    {
        [Key]
        public string IdA { get; set; }

        [Required]
        public string Nome { get; set; }

        public IdentityUser IdANavegation { get; set; }

        public virtual ICollection<Suspensões> Suspensos { get; set; }
    }
}

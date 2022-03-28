using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_API
{
    public class Cliente
    {
        [Key]
        public string IdC { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(9, MinimumLength = 9)]
        public int Telefone { get; set; }

        [Required]
        public string Foto { get; set; }

        [ForeignKey(nameof(IdC))]
        public virtual IdentityUser IdCNavegation { get; set; }

        public ICollection<Layout> Layouts { get; set; }

        public bool Suspenso { get; set; } = false;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS
{
    public class Cliente
    {
        public string IdC { get; set; }

        public int Telefone { get; set; }

        public string Foto { get; set; }

        public ICollection<Layout> Layouts { get; set; }

        public bool Suspenso { get; set; } = false;
    }
}

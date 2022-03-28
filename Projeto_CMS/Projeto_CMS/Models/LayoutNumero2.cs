using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS
{
    public class LayoutNumero2
    {
        [Key]
        public int Id { get; set; }
        
        public string Titulo { get; set; }

        
        public string CorTitulo { get; set; }

        //[Required]
        //public ICollection<Produto> Produtos { get; set; }
        
        public string Descricao { get; set; }

        
        public string CorDescricao { get; set; }

        //[Required]
        //public string CorTextoProdutos { get; set; }

        public string ClienteId { get; set; }
        public Cliente Cliente { get; set; }

    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_API
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DescricaoProduto { get; set; }

        [Required]
        public float Preco { get; set; }

        [Required]
        [StringLength(50)]
        public string Foto { get; set; }

        [Required]
        [NotMapped]
        public IFormFile FotoFile { get; set; }

        [Required]
        
        public int LayoutNumero1Id { get; set; }

        //[ForeignKey(nameof(Id))]
        public Layout LayoutNumero1 { get; set; }
    }

    public class ProdutoRequest
    {
        public IEnumerable<Produto> Produtos { get; set; }
    }
}

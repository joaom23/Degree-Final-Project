using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Projeto_CMS
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DescricaoProduto { get; set; }

        [Required]
        public string Preco { get; set; }

        [Required]
        [StringLength(50)]
        public string Foto { get; set; }

        public string CorProduto { get; set; }
        public ImageSource Imagem { get; set; }

        public int LayoutNumero1Id { get; set; }
        public Layout LayoutNumero1 { get; set; }
    }

    public class ProdutoRequest
    {
        public IEnumerable<Produto> Produtos { get; set; }
    }
}

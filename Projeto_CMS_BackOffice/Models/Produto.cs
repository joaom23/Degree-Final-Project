using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice.Models
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

        [StringLength(50)]
        public string Foto { get; set; }

        [Required]
        public IFormFile FotoFile { get; set; }

        public string CorTexto { get; set; }
        public int LayoutId { get; set; }
        public Layout Layout { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS
{
    public class Layout
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string CorTitulo { get; set; }
        public string Descricao { get; set; }
        public string CorDescricao { get; set; }
        public string CorTextoProdutos { get; set; }
        public string Morada { get; set; }
        public string CorMorada { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraFecho { get; set; }
        public string CorHorario { get; set; }

        public string CorDeFundo { get; set; }
        public string FotoBanner { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public string ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int NumeroLayout { get; set; }

    }
}

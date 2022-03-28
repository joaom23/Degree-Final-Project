using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_API
{
    public class SuspenderUserViewModel
    {
        [Required]
        public string IdUser { get; set; }

        [Required]
        public string IdAdmin { get; set; }

        [Required]
        public int Dias { get; set; }

        [Required]
        public string Motivo { get; set; }
    }
}

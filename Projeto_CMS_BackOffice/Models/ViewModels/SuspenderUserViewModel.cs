using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice.Models.ViewModels
{
    public class SuspenderUserViewModel
    {
        [Required]
        public int IdUser { get; set; }

        [Required]
        public string IdAdmin { get; set; }

        [Required]
        public string Dias { get; set; }

        [Required]
        public string Motivo { get; set; }
    }
}

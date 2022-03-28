using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_API.Models.ViewModels
{
    public class AlterarFotoPerfilViewModel
    {
        public string UserId { get; set; }
        public string Foto { get; set; }
        public IFormFile FotoFile { get; set; }
    }
}

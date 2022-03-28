using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice.Models
{
    public class Response
    {
        public IEnumerable<string> Errors { get; set; }
        public string Message { get; set; }
        public bool IsSucess { get; set; }
    }
}

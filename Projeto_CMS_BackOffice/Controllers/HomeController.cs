using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Projeto_CMS_API.Models;
using Projeto_CMS_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Projeto_CMS_BackOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _APIserver;
        private readonly HttpClient _client;
        public bool Admin { get; set; }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("CMS_API").Value;
            _client = new HttpClient();
            Admin = false;
        }

       public async Task<IActionResult> Index(string Type, string Message)
        {
            var userImage = HttpContext.Session.GetString("Foto");

           await HasAdmin();

            ViewBag.Admin = Admin;

            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View();
        }

        public async Task HasAdmin()
        {
            var response = await _client.GetAsync(_APIserver + "/api/utilizadores/existadmin");

            var responsebody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<bool>(responsebody);

            Admin = responseObject;
        }
    }
}

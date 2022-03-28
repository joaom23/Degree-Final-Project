using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Projeto_CMS_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _APIserver;
        private readonly IWebHostEnvironment _env;

        public AdministradorController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("CMS_API").Value;
            _client = new HttpClient();
            _env = env;

        }
        public void AddToken()
        {
            var authToken = HttpContext.Session.GetString("Token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PerfilAdmin(string userId, string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            AddToken();

            var response = await _client.GetAsync(_APIserver + "/api/administrador/perfiladmin/" + userId);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var user = JsonConvert.DeserializeObject<Administrador>(responsebody);

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> GerirClientes(string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            AddToken();

            var response = await _client.GetAsync(_APIserver + "/api/administrador/getactiveusers");

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var users = JsonConvert.DeserializeObject<ReturnUsersViewmodel>(responsebody);

            List<Cliente> clientes = new List<Cliente>();

            foreach (var u in users.Clientes)
            {
                clientes.Add(u);
            }

            return View(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> SuspenderCliente(IFormCollection dados)
        {
            var adminId = HttpContext.Session.GetString("Id");

            var userId = dados["userId"];
            var motivo = dados["Motivo"];
            var dias = dados["Dias"];

            AddToken();

            var formContent = new MultipartFormDataContent
                {
                     { new StringContent(userId), "IdUser" },
                     { new StringContent(adminId), "IdAdmin" },
                     { new StringContent(motivo), "Motivo" },
                     { new StringContent(dias.ToString()), "Dias" }
            };

            var response = await _client.PostAsync(_APIserver + "/api/administrador/suspendercliente/", formContent);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            if (responseObject.IsSucess)
            {
                return RedirectToAction("GerirClientes", new { Type = "success", Message = responseObject.Message });
            }
            else
            {
                return RedirectToAction("GerirClientes", new { Type = "danger", Message = responseObject.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoverSuspensaoCliente(string userId)
        {
            AddToken();

            var formContent = new MultipartFormDataContent
                {
                     { new StringContent(userId), "userId" },
            };

            var response = await _client.PostAsync(_APIserver + "/api/administrador/RemoverSuspensaoCliente/", formContent);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            if (responseObject.IsSucess)
            {
                return RedirectToAction("GerirClientes", new { Type = "success", Message = responseObject.Message });
            }
            else
            {
                return RedirectToAction("GerirClientes", new { Type = "danger", Message = responseObject.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Projeto_CMS_API.Models;
using Projeto_CMS_BackOffice.Filters;
using Projeto_CMS_BackOffice.Models;
using Projeto_CMS_BackOffice.Models.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice.Controllers
{
    public class UtilizadoresController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _APIserver;
        private readonly HttpClient _client;
        public bool Admin { get; set; }


        public UtilizadoresController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _APIserver = configuration.GetSection("WebAPIServers").GetSection("CMS_API").Value;
            _client = new HttpClient();
            Admin = false;
        }

        public IActionResult Index(string Type, string Message)
        {
            ViewBag.Message = Message;
            ViewBag.Type = Type;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RegistAdmin(string Type, string Message)
        {
            await HasAdmin();

            if (Admin && HttpContext.Session.GetString("Role") != "Admin")
            {
                return View("Sempermissoes");
            }

            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistAdmin(IFormCollection dados, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                RegistAdminViewmodel user = new RegistAdminViewmodel
                {
                    Email = dados["Email"],
                    Password = dados["Password"],
                    ConfirmPassword = dados["ConfirmPassword"],
                    Nome = dados["Nome"]
                };

                var formContent = new MultipartFormDataContent
                {
                     { new StringContent(user.Email), "Email" },
                     { new StringContent(user.Password), "Password" },
                     { new StringContent(user.ConfirmPassword), "ConfirmPassword" },
                     { new StringContent(user.Nome), "Nome" }
                };

                var response = await _client.PostAsync(_APIserver + "/api/utilizadores/registadmin", formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSucess)
                {
                    return RedirectToAction("Index", "Home", new { Type = "success", Message = responseObject.Message });
                }
                else
                {
                    return RedirectToAction("RegistAdmin", "Utilizadores", new { Type = "danger", Message = responseObject.Message });
                }

            }
            return View();
        }

        [HttpGet]
        public IActionResult Register(string Type, string Message)
        {

            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormCollection dados, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                RegisterViewModel user = new RegisterViewModel
                {
                    Email = dados["Email"],
                    Password = dados["Password"],
                    ConfirmPassword = dados["ConfirmPassword"],
                    Telefone = Convert.ToInt32(dados["Telefone"])
                };

                var formContent = new MultipartFormDataContent
                {
                     { new StringContent(user.Email), "Email" },
                     { new StringContent(user.Password), "Password" },
                     { new StringContent(user.ConfirmPassword), "ConfirmPassword" },
                     { new StringContent(user.Telefone.ToString()), "Telefone" },
                     { new StringContent(Path.GetFileName(foto.FileName)), "Foto" },
                     { new StreamContent(foto.OpenReadStream()), "FotoFile", Path.GetFileName(foto.FileName) },
                };

                var response = await _client.PostAsync(_APIserver + "/api/utilizadores/register", formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSucess)
                {
                    return RedirectToAction("Index", "Home", new { Type = "success", Message = responseObject.Message });
                }
                else
                {
                    return RedirectToAction("Register", "Utilizadores", new { Type = "danger", Message = responseObject.Message });
                }

            }
            return View();
        }

        [HttpGet]

        public IActionResult Login(string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection dados)
        {
            if (ModelState.IsValid)
            {
                LoginViewModel user = new LoginViewModel
                {
                    Email = dados["Email"],
                    Password = dados["Password"]
                };

                var data = new StringContent(
                   JsonConvert.SerializeObject(user, Formatting.Indented),
                    Encoding.UTF8,
                    "application/json");

                var response = await _client.PostAsync(_APIserver + "/api/utilizadores/login", data);

                var responsebody = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSucess)
                {
                    var token = responseObject.Message;

                    var readToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                    var userId = readToken.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                    var userEmail = readToken.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                    var userRole = readToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
                    
                    if(userRole == "Cliente")
                    { 
                        var ImagemUser = readToken.Claims.FirstOrDefault(x => x.Type == "Foto").Value;
                        HttpContext.Session.SetString("Foto", ImagemUser);
                    }
                    else if(userRole == "Admin")
                    {
                        var nomeAdmin = readToken.Claims.FirstOrDefault(x => x.Type == "Nome").Value;
                        HttpContext.Session.SetString("NomeAdmin", nomeAdmin);
                    }

                    HttpContext.Session.SetString("Token", token.ToString());
                    HttpContext.Session.SetString("Id", userId);
                    HttpContext.Session.SetString("Role", userRole);                    
                    HttpContext.Session.SetString("Email", userEmail);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", new { Type = "danger", Message = responseObject.Message });
                }

            }

            return View();

        }

        public async Task<IActionResult> SubmeterEmailRecuperarPassword(IFormCollection dados)
        {
            var email = dados["Email"];

            var formContent = new MultipartFormDataContent
                {
                     { new StringContent(email), "Email" }
            };
            var response = await _client.PostAsync(_APIserver + $"/api/utilizadores/recuperarpassword/", formContent);

            var responsebody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            if (responseObject.IsSucess)
            {
                return RedirectToAction("Login", new { Type = "success", Message = responseObject.Message });
            }
            else
            {
                return RedirectToAction("Login", new { Type = "danger", Message = responseObject.Message });
            }
        }
        public IActionResult RecuperarPassword()
        {
            var permitido = Request.Query["permitido"];

            if (string.IsNullOrEmpty(permitido))
            {
                return View("Sempermissoes");
            }

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> RecuperarPassword(IFormCollection dados)
        {
            if (ModelState.IsValid)
            {
                RecuperarPasswordViewModel passwordViewModel = new RecuperarPasswordViewModel
                {
                    Email = dados["Email"],
                    Token = dados["Token"],
                    NewPassword = dados["NewPassword"],
                    ConfirmNewPassword = dados["ConfirmNewPassword"]
                };


                var data = new StringContent(
                   JsonConvert.SerializeObject(passwordViewModel, Formatting.Indented),
                    Encoding.UTF8,
                    "application/json");

                var response = await _client.PostAsync(_APIserver + "/api/utilizadores/resetarpassword", data);

                var responsebody = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSucess)
                {
                    return RedirectToAction("Login", new { Type = "success", Message = responseObject.Message });
                }
                else
                {
                    return RedirectToAction("Login", new { Type = "danger", Message = responseObject.Message });
                }

            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("CookieDeSessao");
            return RedirectToAction("Index", "Home");
        }

        public static bool Autenticado(HttpContext context)
        {
            if (context.Session.GetInt32("Token") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Administrador(HttpContext context)
        {
            if (context.Session.GetString("Estatuto") == "Administrador")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Cliente(HttpContext context)
        {
            if (context.Session.GetString("Estatuto") == "Cliente")
            {
                return true;
            }
            else
            {
                return false;
            }
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


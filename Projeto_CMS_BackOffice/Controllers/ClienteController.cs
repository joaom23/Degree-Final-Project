using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Projeto_CMS_BackOffice.Filters;
using Projeto_CMS_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Projeto_CMS_BackOffice.Controllers
{
    //[Roles(Role = "Cliente, Admin")]
    public class ClienteController : Controller
    {

        private readonly HttpClient _client;
        private readonly string _APIserver;
        private readonly IWebHostEnvironment _env;
        
        public ClienteController(IConfiguration configuration, IWebHostEnvironment env)
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

        public IActionResult CriarLayout1()
        {
            return View();
        }

        public IActionResult CriarLayoutNumero1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLayoutNumero1(IFormCollection dados)
        {
            var id = HttpContext.Session.GetString("Id");

            if (ModelState.IsValid)
            {
                var formContent = new MultipartFormDataContent();

                var numeroDoProduto = 1;
                var i = 0;

                Layout layout = new Layout
                {
                    Titulo = dados["Titulo"],
                    Descricao = dados["Descricao"],
                    ClienteId = id,
                    CorDescricao = dados["CorDescricao"],
                    CorTitulo = dados["CorTitulo"],
                    CorTextoProdutos = dados["CorProdutos"],
                    Morada = dados["Morada"],
                    CorMorada = dados["CorMorada"],
                    HoraAbertura = TimeSpan.Parse(dados["HoraAbertura"]),
                    HoraFecho = TimeSpan.Parse(dados["HoraFecho"]),
                    CorHorario = dados["CorHorario"],
                    CorDeFundo = dados["CorDeFundo"],
                    NumeroLayout = Convert.ToInt32(dados["NumeroLayout"])

                };

                formContent.Add(new StringContent(layout.ClienteId.ToString()), "ClienteId");
                formContent.Add(new StringContent(layout.Titulo), "Titulo");
                formContent.Add(new StringContent(layout.Descricao), "Descricao");
                formContent.Add(new StringContent(layout.CorDescricao), "CorDescricao");
                formContent.Add(new StringContent(layout.CorTitulo), "CorTitulo");
                formContent.Add(new StringContent(layout.CorTextoProdutos), "CorTextoProdutos");
                formContent.Add(new StringContent(layout.Morada), "Morada");
                formContent.Add(new StringContent(layout.CorMorada), "CorMorada");
                formContent.Add(new StringContent(layout.HoraAbertura.ToString()), "HoraAbertura");
                formContent.Add(new StringContent(layout.HoraFecho.ToString()), "HoraFecho");
                formContent.Add(new StringContent(layout.CorHorario), "CorHorario");
                formContent.Add(new StringContent(layout.NumeroLayout.ToString()), "NumeroLayout");
                formContent.Add(new StringContent(layout.CorDeFundo), "CorDeFundo");
                formContent.Add(new StringContent(Path.GetFileName(dados.Files["FotoBanner"].FileName)), "FotoBanner");
                formContent.Add(new StreamContent(dados.Files["FotoBanner"].OpenReadStream()), "FotoBannerFile", Path.GetFileName(dados.Files["FotoBanner"].FileName));

                foreach (var dado in dados.Files)
                {
                    if (dado.Name != "FotoBanner")
                    {
                        Produto produto = new Produto
                        {
                            DescricaoProduto = dados["DescricaoProduto" + numeroDoProduto],
                            Preco = float.Parse(dados["Preco" + numeroDoProduto]),
                            LayoutId = layout.Id
                        };


                        //Adiciona elementos simples de texto

                        formContent.Add(new StringContent(produto.Preco.ToString()), "Produtos[" + i + "].Preco");
                        formContent.Add(new StringContent(produto.DescricaoProduto), "Produtos[" + i + "].DescricaoProduto");
                        formContent.Add(new StringContent(Path.GetFileName(dado.FileName)), "Produtos[" + i + "].Foto");
                        formContent.Add(new StreamContent(dado.OpenReadStream()), "Produtos[" + i + "].FotoFile", Path.GetFileName(dado.FileName));

                        numeroDoProduto++;
                        i++;
                    }
                }

                AddToken();

                var response = await _client.PostAsync(_APIserver + "/api/cliente/AddLayoutNumero1", formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("SemPermissoes");
                }

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSucess && responseObject != null)
                {
                    return RedirectToAction("Index", "Home", new { Type = "success", Message = responseObject.Message });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { Type = "danger", Message = responseObject.Message });
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditarLayoutNumero1(int LayoutId, string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            AddToken();

            var response = await _client.GetAsync(_APIserver + "/api/cliente/Getlayoutnumero1/" + LayoutId);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var layout = JsonConvert.DeserializeObject<Layout>(responsebody);

            return View(layout);
        }

        [HttpPost]
        public async Task<IActionResult> EditarLayoutNumero1(IFormCollection dados)
        {
            var id = HttpContext.Session.GetString("Id");

            if (ModelState.IsValid)
            {
                var formContent = new MultipartFormDataContent();

                var produtosExistentes = Convert.ToInt32(dados["produtosExistentes"]); //produtos que ja exisitiam no layout
                var numeroDeProdutos = Convert.ToInt32(dados["totalProdutos"]); //total de produtos no novo layout
                var i = 0;

                Layout layout = new Layout
                {
                    Id = Convert.ToInt32(dados["Id"]),
                    Titulo = dados["Titulo"],
                    Descricao = dados["Descricao"],
                    ClienteId = id,
                    CorDescricao = dados["CorDescricao"],
                    CorTitulo = dados["CorTitulo"],
                    CorTextoProdutos = dados["CorProdutos"],
                    Morada = dados["Morada"],
                    CorMorada = dados["CorMorada"],
                    HoraAbertura = TimeSpan.Parse(dados["HoraAbertura"]),
                    HoraFecho = TimeSpan.Parse(dados["HoraFecho"]),
                    CorHorario = dados["CorHorario"],
                    FotoBanner = dados["FotoBanner"],
                    CorDeFundo = dados["CorDeFundo"],
                    NumeroLayout = Convert.ToInt32(dados["NumeroLayout"])

                };

                formContent.Add(new StringContent(layout.Id.ToString()), "Id");
                formContent.Add(new StringContent(layout.ClienteId.ToString()), "ClienteId");
                formContent.Add(new StringContent(layout.Titulo), "Titulo");
                formContent.Add(new StringContent(layout.Descricao), "Descricao");
                formContent.Add(new StringContent(layout.CorDescricao), "CorDescricao");
                formContent.Add(new StringContent(layout.CorTitulo), "CorTitulo");
                formContent.Add(new StringContent(layout.CorTextoProdutos), "CorTextoProdutos");
                formContent.Add(new StringContent(layout.Morada), "Morada");
                formContent.Add(new StringContent(layout.CorMorada), "CorMorada");
                formContent.Add(new StringContent(layout.HoraAbertura.ToString()), "HoraAbertura");
                formContent.Add(new StringContent(layout.HoraFecho.ToString()), "HoraFecho");
                formContent.Add(new StringContent(layout.CorHorario), "CorHorario");
                formContent.Add(new StringContent(layout.CorDeFundo), "CorDeFundo");
                formContent.Add(new StringContent(layout.NumeroLayout.ToString()), "NumeroLayout");


                var fotoBannerAntiga = dados["FotoBannerAntiga"].ToString();
                var fotoBanner = "";
                if (dados.Files["FotoBanner"] != null)
                {
                   fotoBanner = dados.Files["FotoBanner"].FileName.ToString();
                }

                if (fotoBanner == "" || fotoBanner == fotoBannerAntiga)
                {                 
                    formContent.Add(new StringContent(fotoBannerAntiga), "FotoBanner");
                    formContent.Add(new StreamContent(Stream.Null), "FotoBannerFile", fotoBannerAntiga);
                }
                else
                {
                    formContent.Add(new StringContent(Path.GetFileName(dados.Files["FotoBanner"].FileName)), "FotoBanner");
                    formContent.Add(new StreamContent(dados.Files["FotoBanner"].OpenReadStream()), "FotoBannerFile", Path.GetFileName(dados.Files["FotoBanner"].FileName));
                }

                for (int j = 1; j <= numeroDeProdutos; j++)
                {
                    if (dados["Preco" + j].ToString() != "")
                    {

                        Produto produto = new Produto
                        {
                            Id = Convert.ToInt32(dados["IdProduto" + j]),
                            DescricaoProduto = dados["DescricaoProduto" + j],
                            Preco = float.Parse(dados["Preco" + j]),
                            LayoutId = layout.Id
                        };


                            formContent.Add(new StringContent(produto.Id.ToString()), "Produtos[" + i + "].Id");
                            formContent.Add(new StringContent(produto.Preco.ToString()), "Produtos[" + i + "].Preco");
                            formContent.Add(new StringContent(produto.DescricaoProduto), "Produtos[" + i + "].DescricaoProduto");

                            if (dados.Files.Count != 0)
                            {
                                var k = 1;
                                foreach (var dado in dados.Files)
                                {
                                    var totalFicheiros = dados.Files.Count;


                                    if (dado.FileName != "FotoBanner")
                                    {
                                        if (dado.Name == "Foto" + j)
                                        {
                                            formContent.Add(new StringContent(Path.GetFileName(dado.FileName)), "Produtos[" + i + "].Foto");
                                            formContent.Add(new StreamContent(dado.OpenReadStream()), "Produtos[" + i + "].FotoFile", Path.GetFileName(dado.FileName));
                                            break;
                                        }
                                        else if (j <= produtosExistentes && k == totalFicheiros)
                                        {
                                            var fotoName = dados["FotoN" + j];

                                            formContent.Add(new StringContent(fotoName), "Produtos[" + i + "].Foto");
                                            formContent.Add(new StreamContent(Stream.Null), "Produtos[" + i + "].FotoFile", fotoName);


                                        }
                                    }

                                    k++;
                                }
                            }
                            else
                            {
                                var fotoName = dados["FotoN" + j];

                                formContent.Add(new StringContent(fotoName), "Produtos[" + i + "].Foto");
                                formContent.Add(new StreamContent(Stream.Null), "Produtos[" + i + "].FotoFile", fotoName);
                            }

                            i++;
                    }
                }

                AddToken();

                var response = await _client.PostAsync(_APIserver + "/api/cliente/EditarLayoutNumero1", formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("SemPermissoes");
                }

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSucess && responseObject != null)
                {
                    return RedirectToAction("EditarLayoutNumero1", "Cliente", new {LayoutId = layout.Id ,Type = "success", Message = responseObject.Message });
                }
                else
                {
                    return RedirectToAction("EditarLayoutNumero1", "Cliente", new {LayoutId = layout.Id ,Type = "danger", Message = responseObject.Message });
                }
            }
            return View();
        }

        public async Task<IActionResult> RemoverProduto(int LayoutId, int ProdutoId)
        {
            AddToken();

            var response = await _client.GetAsync(_APIserver + "/api/cliente/RemoverProduto/" + LayoutId + "/" + ProdutoId);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            Layout layout = JsonConvert.DeserializeObject<Layout>(responsebody);

            return View("EditarLayoutNumero1", layout);
        }

    
        public async Task<IActionResult> SeusLayouts()
        {
            var id = HttpContext.Session.GetString("Id");

            AddToken();

            var response = await _client.GetAsync(_APIserver + "/api/cliente/SeusLayouts/" + id);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var layout = JsonConvert.DeserializeObject<ReturnLayoutsViewModel>(responsebody);

            return View(layout);

        }

        [HttpGet]
        public async Task<IActionResult> PerfilCliente(string userId, string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            AddToken();

            var response = await _client.GetAsync(_APIserver + "/api/cliente/perfil/" + userId);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var user = JsonConvert.DeserializeObject<Cliente>(responsebody);

            return View(user);
        }

        public async Task<IActionResult> SubmeterAlterarPalavraPasse(IFormCollection dados)
        {
            var userId = HttpContext.Session.GetString("Id");

            AlterarPalavraPassViewModel pass = new AlterarPalavraPassViewModel()
            {
                AntigaPassword = dados["passAntiga"],
                NovaPassword = dados["passNova"],
                ConfirmPassword = dados["passNovaConfirmar"]
            };
            
            var formContent = new MultipartFormDataContent();

            formContent.Add(new StringContent(userId.ToString()), "UserId");
            formContent.Add(new StringContent(pass.AntigaPassword), "AntigaPassword");
            formContent.Add(new StringContent(pass.NovaPassword), "NovaPassword");
            formContent.Add(new StringContent(pass.ConfirmPassword), "ConfirmPassword");

            AddToken();

            var response = await _client.PostAsync(_APIserver + "/api/utilizadores/AlterarPalavraPass", formContent);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            if (responseObject.IsSucess && responseObject != null)
            {
                if(HttpContext.Session.GetString("Role") == "Cliente")
                {
                    return RedirectToAction("PerfilCliente", "Cliente", new { userId = userId, Type = "success", Message = responseObject.Message });
                }
                else if (HttpContext.Session.GetString("Role") == "Admin")
                {
                    return RedirectToAction("PerfilAdmin", "Administrador", new { userId = userId, Type = "success", Message = responseObject.Message });
                }              
            }
            else
            {
                return RedirectToAction("PerfilCliente", "Cliente", new { userId = userId, Type = "danger", Message = responseObject.Message });
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MudarFotoPerfil(IFormCollection dados, IFormFile foto)
        {
            var userId = HttpContext.Session.GetString("Id");

            var formContent = new MultipartFormDataContent();

            formContent.Add(new StringContent(userId.ToString()), "UserId");
            formContent.Add(new StringContent(Path.GetFileName(foto.FileName)), "Foto");
            formContent.Add(new StreamContent(foto.OpenReadStream()), "FotoFile", Path.GetFileName(foto.FileName));

            AddToken();

            var response = await _client.PostAsync(_APIserver + "/api/utilizadores/AlterarFotoPerfil", formContent);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            var ImagemUser = responseObject.Message.Split('!')[1];

            HttpContext.Session.SetString("Foto", ImagemUser);

            if (responseObject.IsSucess && responseObject != null)
            {
                return RedirectToAction("PerfilCliente", "Cliente", new { userId = userId, Type = "success", Message = responseObject.Message.Split('!')[0] });
            }
            else
            {
                return RedirectToAction("PerfilCliente", "Cliente", new { userId = userId, Type = "danger", Message = responseObject.Message });
            }
        }
    }
}


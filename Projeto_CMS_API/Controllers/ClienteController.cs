using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Projeto_CMS_API.Models;
using Projeto_CMS_API.Models.ViewModels;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projeto_CMS_API.Controllers
{
    [Authorize(Roles ="Cliente")]
    [Route("/api/[controller]")]
    [ApiController]

    public class ClienteController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly APIContext _context;

        public ClienteController(UserManager<IdentityUser> userManager, IConfiguration configuration, APIContext context, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _environment = environment;

        }

        [HttpPost("AddLayoutNumero1")]
        public async Task<IActionResult> AddLayoutNumero1([FromForm] Layout layout)
        {
            if (ModelState.IsValid)
            {
                if (layout == null)
                {
                    throw new NullReferenceException("Produto model não existe!");
                }
                //Guardar foto do banner
                layout.FotoBanner = await SaveImage(layout.FotoBannerFile);


                //Guardar foto dos produtos
                foreach (var p in layout.Produtos)
                {
                    p.Foto = await SaveImage(p.FotoFile);
                }


                _context.Layout.Add(layout);
                await _context.SaveChangesAsync();

                return Ok(new Response
                {
                    Message = "Layout criado com sucesso!",
                    IsSucess = true
                });

            }

            return BadRequest(new Response
            {
                Message = "Erro na adição do produto",
                IsSucess = false,
            });
        }

        [HttpGet("GetLayoutnumero1/{LayoutId}")]

        public async Task<ActionResult<Layout>> GetLayoutNumero1(int LayoutId)
        {
            var layout = await _context.Layout.Include(x=>x.Produtos).FirstOrDefaultAsync(x => x.Id == LayoutId);

            if (User.FindFirstValue("Id") != layout.ClienteId)
            {
                return Unauthorized(new Response { IsSucess = false, Message = "Não tem permissões para alterar efeturar esta ação." });
            }

            if (layout == null)
            {
                return BadRequest(new Response { IsSucess = false, Message = "Utilizador nao tem nenhum layout" });
            }

            return layout;
        }

        [HttpGet("RemoverProduto/{LayoutId}/{ProdutoId}")]

        public async Task<ActionResult<Layout>> RemoverProduto(int LayoutId, int ProdutoId )
        {
            var layout = await _context.Layout.Include(x => x.Produtos).FirstOrDefaultAsync(x => x.Id == LayoutId);

            if (User.FindFirstValue("Id") != layout.ClienteId)
            {
                return Unauthorized(new Response { IsSucess = false, Message = "Não tem permissões para alterar efeturar esta ação." });
            }

            var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == ProdutoId);

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            if (layout == null || produto == null)
            {
                return BadRequest(new Response { IsSucess = false, Message = "Erro ao eliminar o produto" });
            }

            return layout;
        }

        [HttpPost("EditarLayoutNumero1")]
        public async Task<IActionResult> EditarLayoutNumero1([FromForm] Layout layout)
        {
            if (User.FindFirstValue("Id") != layout.ClienteId)
            {
                return Unauthorized(new Response { IsSucess = false, Message = "Não tem permissões para alterar efeturar esta ação." });
            }

            if (ModelState.IsValid)
            {
                if (layout == null)
                {
                    throw new NullReferenceException("Produto model não existe!");
                }

                Layout layoutEditar = _context.Layout.AsNoTracking().FirstOrDefault(x => x.Id == layout.Id);

                if(layoutEditar.FotoBanner != layout.FotoBanner)
                {
                    layoutEditar.FotoBanner = await SaveImage(layout.FotoBannerFile);
                }

                foreach (var p in layout.Produtos)
                {
                    //Verificar se o produto já existe, se nao existir cria um novo e adiciona a foto
                    if (!_context.Produtos.Any(x => x.Id == p.Id))
                    {
                        p.Foto = await SaveImage(p.FotoFile);
                    }
                    else
                    {
                        //Se já existir atuliza os valores e se a imagem for alterada acrescenta-a (apagar foto antiga de for alterada?)
                        
                        
                        if (!_context.Produtos.Any(x=>x.Foto == p.Foto))
                        {
                            p.Foto = await SaveImage(p.FotoFile);
                        }
                    }


                }

                layoutEditar.Titulo = layout.Titulo;
                layoutEditar.Descricao = layout.Descricao;
                layoutEditar.CorDescricao = layout.CorDescricao;
                layoutEditar.CorTextoProdutos = layout.CorTextoProdutos;
                layoutEditar.CorTitulo = layout.CorTitulo;
                layoutEditar.Morada = layout.Morada;
                layoutEditar.CorMorada = layout.CorMorada;
                layoutEditar.HoraAbertura = layout.HoraAbertura;
                layoutEditar.HoraFecho = layout.HoraFecho;
                layoutEditar.CorHorario = layout.CorHorario;
                layoutEditar.CorDeFundo = layout.CorDeFundo;
                layoutEditar.NumeroLayout = layout.NumeroLayout;
                layoutEditar.Produtos = layout.Produtos;

                _context.Layout.Update(layoutEditar);
                await _context.SaveChangesAsync();

                return Ok(new Response
                {
                    Message = "Layout editado com sucesso!",
                    IsSucess = true
                });

            }

            return BadRequest(new Response
            {
                Message = "Erro na edição do layout",
                IsSucess = false,
            });
        }

        [HttpGet("SeusLayouts/{userId}")]

        public async Task<ActionResult<ReturnLayoutsViewModel>> SeusLayouts(string userId)
        {
            if (User.FindFirstValue("Id") != userId)
            {
                return Unauthorized(new Response { IsSucess = false, Message = "Não tem permissões para alterar efeturar esta ação." });
            }

            ReturnLayoutsViewModel layouts = new ReturnLayoutsViewModel
            {
                LayoutNumero1s = await _context.Layout.Include(x => x.Produtos).Where(x => x.ClienteId == userId).ToListAsync(),
               
            };

            if (layouts == null)
            {
                return BadRequest(new Response { IsSucess = false, Message = "Utilizador nao tem nenhum layout" });
            }

            return layouts;
        }

        [HttpGet("perfil/{id}")]
        public async Task<ActionResult<Cliente>> Perfil(string id)
        {
            if (User.FindFirstValue("Id") != id)
            {
                return Unauthorized(new Response { IsSucess = false, Message = "Não tem permissões para alterar efeturar esta ação." });
            }

            var perfil = await _context.Clientes.Include(x=>x.IdCNavegation).Include(x=>x.Layouts).FirstOrDefaultAsync(x => x.IdC == id); 

            if (perfil == null)
            {
                return BadRequest();
            }

            return perfil;
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile image)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(image.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(_environment.ContentRootPath, "Recursos/ImagensLayouts", imageName);
            using (var filestream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(filestream);
            }
            return imageName;
        }

        [NonAction]
        [AllowAnonymous]
        public IFormFile GetImage(string filename)
        {
            var imagePath = Path.Combine(_environment.ContentRootPath, "Recursos/ImagensLayouts", filename);
            var imagem = System.IO.File.ReadAllBytes(imagePath);

            var stream = new MemoryStream(imagem);

            var file = new FormFile(stream, 0, imagem.Length, null, filename)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            return file;
        }

        [HttpGet]
        [Route("[action]/{filename}")]
        [AllowAnonymous]
        public IActionResult ImageGetLayout(string filename)
        {
            var image = System.IO.File.OpenRead("Recursos/ImagensLayouts/" + filename);

            return File(image, "image/jpeg");
        }

        [HttpGet]
        [Route("[action]/{filename}")]
        [AllowAnonymous]
        public IActionResult ImageGetUser(string filename)
        {
            var image = System.IO.File.OpenRead("Recursos/ImagensClientes/" + filename);

            return File(image, "image/jpeg");
        }

        [HttpGet]
        [Route("[action]/{filename}")]
        [AllowAnonymous]
        public IActionResult ImageGetDefault(string filename)
        {
            var image = System.IO.File.OpenRead("Recursos/ImagensDefault/" + filename);

            return File(image, "image/jpeg");
        }
    }
}

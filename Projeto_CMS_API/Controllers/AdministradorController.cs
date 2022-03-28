using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Projeto_CMS_API.Models;
using Projeto_CMS_API.Models.ViewModels;
using Projeto_CMS_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CMS_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/api/[controller]")]
    [ApiController]
    public class AdministradorController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IWebHostEnvironment _environment;
        private readonly APIContext _context;
        private readonly IEmailService _mailservice;

        public AdministradorController(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> rolemanager,
            IWebHostEnvironment environment,
            APIContext context,
            IEmailService mailservice)
        {
            _userManager = userManager;
            _configuration = configuration;
            _rolemanager = rolemanager;
            _environment = environment;
            _context = context;
            _mailservice = mailservice;
        }

        [HttpPost("RegistAdmin")]
        public async Task<IActionResult> RegistAdmin([FromForm] RegistAdminViewmodel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Register model não existe");
            }

            var u = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);

            if (u != null)
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = $"Já existe um utlizador com o email {model.Email}"
                });
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new Response
                {
                    Message = "Palavras passe não coincidem",
                    IsSucess = false
                });

            }
            IdentityUser user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            await _userManager.AddToRoleAsync(user, "Admin");

            Administrador admin = new Administrador
            {
                IdA = user.Id,
                Nome = model.Nome
            };

            _context.Administradors.Add(admin);
            await _context.SaveChangesAsync();

            if (result.Succeeded)
            {
                return Ok(new Response
                {
                    Message = "Conta criada com sucesso!.",
                    IsSucess = true

                });
            }

            return BadRequest(new Response
            {
                Message = "Erro na criação do utilizador",
                IsSucess = false,
            });
        }

        [HttpGet]
        [Route("[action]/{filename}")]
        [AllowAnonymous]
        public IActionResult ImageGetDefault(string filename)
        {
            var image = System.IO.File.OpenRead("Recursos/ImagensDefault/" + filename);

            return File(image, "image/jpeg");
        }

        [HttpGet("perfiladmin/{id}")]
        public async Task<ActionResult<Administrador>> PerfilAdmin(string id)
        {

            var perfil = await _context.Administradors.Include(x => x.IdANavegation).FirstOrDefaultAsync(x => x.IdA == id);

            if (perfil == null)
            {
                return BadRequest();
            }
            //else if (_userManager.GetUserId(HttpContext.User) != id)
            //{
            //    return Unauthorized(new Response { IsSucess = false, Message = "Não tem permissões para ver este perfil" });
            //}

            return perfil;
        }

        [HttpGet("GetActiveUsers")]
        public async Task<ActionResult<ReturnUsersViewmodel>> GetActiveUsers(string id)
        {
            ReturnUsersViewmodel users = new ReturnUsersViewmodel()
            {
                 Clientes = await _context.Clientes.Include(x => x.IdCNavegation).Where(x=>x.IdCNavegation.EmailConfirmed == true).ToListAsync()
            };

            if (users == null)
            {
                return BadRequest();
            }

            return users;
        }

        [HttpPost("SuspenderCliente")]
        public async Task<IActionResult> SuspenderCliente([FromForm] SuspenderUserViewModel suspensao)
        {
            if (suspensao == null)
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = "Erro ao suspender o cliente."
                });
            }

            var userSuspender = _context.Clientes.FirstOrDefault(x => x.IdC == suspensao.IdUser);

            Suspensões novaSuspensao = new Suspensões()
            {
                IdAdm = suspensao.IdAdmin,
                IdU = suspensao.IdUser,
                Motivo = suspensao.Motivo,
                DataBloqueio = DateTime.Now.AddDays(suspensao.Dias)
            };

            userSuspender.Suspenso = true;

            try
            {
            _context.Clientes.Update(userSuspender);
            _context.Suspensões.Add(novaSuspensao);
            await _context.SaveChangesAsync();

                return Ok(new Response
                {
                    IsSucess = true,
                    Message = "Cliente suspenso com sucesso!"
                });

            }
            catch
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = "Erro ao suspender o cliente."
                });
            }
        }

        [HttpPost("RemoverSuspensaoCliente")]
        public async Task<IActionResult> RemoverSuspensaoCliente([FromForm] string userId)
        {
            if (userId == null)
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = "Erro desbloquear o cliente."
                });
            }

            var userDesbloquear = _context.Clientes.Include(x=>x.IdCNavegation).FirstOrDefault(x => x.IdC == userId);
            var suspensao = _context.Suspensões.FirstOrDefault(x => x.IdU == userId);

            userDesbloquear.Suspenso = false;

            try
            {
                _context.Suspensões.Remove(suspensao);
                _context.Clientes.Update(userDesbloquear);
                await _context.SaveChangesAsync();

                //ENVIAR EMAIL A AVISAR SOBRE O DESBLOQEIO
                var url = "http://localhost:10382/Utilizadores/Login";
               await _mailservice.sendEmailAsync(userDesbloquear.IdCNavegation.Email, "Suspensão Cancelada",
                    "<h1>Fim da suspensão</h1>" + $"<p> A sua suspensão foi cancelada por um dos nossos admins, já está autorizado a utilizar o nosso site novamente. Faça login a partir do seguinte <a href='{url}'>link</a></p>");

                return Ok(new Response
                {
                    IsSucess = true,
                    Message = "Cliente desbloquado com sucesso!"
                });

            }
            catch
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = "Erro ao suspender o cliente."
                });
            }
        }
    }
}

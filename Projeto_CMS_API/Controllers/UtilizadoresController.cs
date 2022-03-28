using IdentityServer4.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Projeto_CMS_API.Models;
using Projeto_CMS_API.Models.ViewModels;
using Projeto_CMS_API.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CMS_API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UtilizadoresController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IWebHostEnvironment _environment;
        private readonly APIContext _context;
        private readonly IEmailService _mailservice;

        public UtilizadoresController(UserManager<IdentityUser> userManager, 
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
                    Message = "A sua conta de admin foi criada com sucesso!.",
                    IsSucess = true

                });
            }

            return BadRequest(new Response
            {
                Message = "Erro na criação do utilizador",
                IsSucess = false,
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Register model não existe");
            }

            var u = await _userManager.Users.FirstOrDefaultAsync(x=>x.Email == model.Email);

            if(u != null)
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = $"Já existe um utlizador com o email {model.Email}"
                });
            }

            var userTelefone = await _context.Clientes.FirstOrDefaultAsync(x => x.Telefone == model.Telefone);

            if(userTelefone != null)
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = $"Já existe um utlizador com o telefone {model.Telefone}"
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
                        
                await _userManager.AddToRoleAsync(user, "Cliente");

            Cliente cliente = new Cliente
            {
                IdC = user.Id,
                Telefone = model.Telefone
            };

            if (model.Foto != null)
            {    
                cliente.Foto = model.Foto;
                cliente.Foto = await SaveImage(model.FotoFile, cliente.IdC);
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            if (result.Succeeded)
                {

                //Enviar email de confirmação

                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var endodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);

                var validEmailToken = WebEncoders.Base64UrlEncode(endodedEmailToken);

                var url = $"{_configuration["AppUrl"]}/api/utilizadores/ConfirmarEmail?userid={user.Id}&token={validEmailToken}";

                await _mailservice.sendEmailAsync(user.Email, "Confirme o seu Email", "<h1>Bem vindo ao nosso CMS de Aplicações Móveis </h1>" +
                    $"<p>Por favor confira o seu email carregando no seguinte link <a href='{url}'>Confirmar Email</a> </p>");

                return Ok(new Response
                    {
                        Message = "Conta criada com sucesso! Por favor confirme o seu email.",
                        IsSucess = true

                    });
                }

                return BadRequest(new Response
                {
                    Message = "Erro na criação do utilizador",
                    IsSucess = false,
                });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                return BadRequest(new Response
                {
                    Message = "Email ou Password incorretos",
                    IsSucess = false
                });
            }

            if (!_userManager.IsInRoleAsync(user, "Admin").Result)
            {
                var verificarSuspensao = _context.Clientes.FirstOrDefault(x => x.IdC == user.Id);

                if(verificarSuspensao.Suspenso == true)
                {
                    var suspensao = _context.Suspensões.FirstOrDefault(x => x.IdU == user.Id);
                    var admin = _context.Administradors.FirstOrDefault(x => x.IdA == suspensao.IdAdm);
                    return Ok(new Response
                    {
                        IsSucess = false,
                        Message = $"Você foi suspenso no dia {suspensao.DataBloqueio} pelo admin {admin.Nome} pelo seguinte motivo: {suspensao.Motivo}"
                    });
                }

                if (!user.EmailConfirmed)
                {
                    return Ok(new Response
                    {
                        IsSucess = false,
                        Message = "Email não confirmado, por favor confirme o seu email."
                    });
                }
            }


            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return BadRequest(new Response
                {
                    Message = "Email ou Password incorretos",
                    IsSucess = false
                });
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();

            if (_userManager.IsInRoleAsync(user, "Cliente").Result)
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.IdC == user.Id);
                var UserImage = cliente.Foto;

                claims.Add(new Claim("Email", model.Email));
                claims.Add(new Claim("Id", user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.Id));
                claims.Add(new Claim("Foto", cliente.Foto));
            }

            if (_userManager.IsInRoleAsync(user, "Admin").Result)
            {
                var admin = await _context.Administradors.FirstOrDefaultAsync(x => x.IdA == user.Id);

                claims.Add(new Claim("Email", model.Email));
                claims.Add(new Claim("Id", user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                claims.Add(new Claim("Nome", admin.Nome));

            }

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var keybuffer = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: new SigningCredentials(keybuffer, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new Response
            {
               Message = tokenAsString,
               IsSucess = true,
               ExpireDate = token.ValidTo
            });
        }

        [HttpGet("ConfirmarEmail")]
        public async Task<IActionResult> ConfirmarEmail(string userId, string token)
        {
            if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = "User não encontrado"
                });
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);

            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return Redirect($"{_configuration["AppUrl"]}/confirmaremail.html");
            }

            return BadRequest(new Response
            {
                IsSucess = false,
                Message = "Erro ao confirmar o email",
            }); 
        }

        [HttpPost("RecuperarPassword")]
        public async Task<IActionResult> RecuperarPasswordAsync([FromForm]string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }

            var user = _userManager.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = $"Não existe nenhum utilizador associado ao email {email}"
                });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Encoding.UTF8.GetBytes(token);

            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            var url = $"http://localhost:10382/utilizadores/recuperarpassword?permitido=ok&email={email}&token={validToken}";

            await _mailservice.sendEmailAsync(email, "Recuperar Password", "<h1>Siga as seguintes intruções para recuperar a sua password</h1>" +
                $"<p>Para recuparar a sua password <a href='{url}'>clique aqui</a> </p>");

            return Ok(new Response 
            {
                IsSucess = true,
                Message = "O link para recuperar a sua password foi enviado para o seu email."
            });
        }

        [HttpPost("ResetarPassword")]
        public async Task<IActionResult> ResetarPasswordAsync([FromBody]RecuperarPasswordViewModel model)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = $"Não existe nenhum utilizador associado ao email {model.Email}"
                });
            }

            if(model.NewPassword != model.ConfirmNewPassword)
            {
                return Ok(new Response 
                {
                    IsSucess = false,
                    Message = "Palavras passe não coincidem",
                });
            }

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new Response
                {
                    IsSucess = true,
                    Message = "Password reposta com sucesso!",
                });
            }

            return BadRequest(new Response
            {
                IsSucess = false,
                Message = "Ups, algo correu mal na recuperação da password",
            });
        }

        [HttpPost("AlterarPalavraPass")]
        public async Task<IActionResult> AlterarPalavraPass([FromForm] AlterarPalavraPassViewModel pass)
        {
            if (pass == null)
            {
                throw new NullReferenceException("Erro ao alterar palavra passe.");
            }

            var user = _userManager.Users.FirstOrDefault(x => x.Id == pass.UserId);

            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = "Não existe nenhum utilizador essa palavra passe"
                });
            }
                
           var result =  await _userManager.ChangePasswordAsync(user, pass.AntigaPassword, pass.NovaPassword);

            if (result.Succeeded)
            {
                if (pass.NovaPassword != pass.ConfirmPassword)
                {
                    return Ok(new Response
                    {
                        IsSucess = false,
                        Message = "Palavras passe não coincidem",
                    });
                }

                return Ok(new Response
                {
                    IsSucess = true,
                    Message = "Password alterada com sucesso!"
                });
            }
            else
            {
                return BadRequest(new Response
                {
                    IsSucess = false,
                    Message = $"A palavra pass antiga está errada."
                });
            }
        }

        [HttpPost("AlterarFotoPerfil")]
        public async Task<IActionResult> AlterarFotoPerfil([FromForm] AlterarFotoPerfilViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Erro ao alterar palavra passe.");
            }

            var user = _context.Clientes.FirstOrDefault(x => x.IdC == model.UserId);

            model.Foto = await SaveImage(model.FotoFile, user.IdC);
            user.Foto = model.Foto;

            _context.Clientes.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new Response()
            {
                IsSucess = true,
                Message = "Foto alterada com sucesso!" + user.Foto
            });

        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile image, string userName)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(image.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + userName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(_environment.ContentRootPath, "Recursos/ImagensClientes", imageName);
            using (var filestream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(filestream);
            }
            return imageName;
        }

        [HttpGet("ExistAdmin")]
        public bool ExistAdmin()
        {
            if (_context.Administradors.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

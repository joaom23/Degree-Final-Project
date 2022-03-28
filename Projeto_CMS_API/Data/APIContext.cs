using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projeto_CMS_API;
using Projeto_CMS_API.Models;

public class APIContext : IdentityDbContext
{
    public APIContext(DbContextOptions<APIContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Layout> Layout { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Administrador> Administradors { get; set; }
    public DbSet<Suspensões> Suspensões { get; set; }

}

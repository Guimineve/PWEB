using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestaoLoja.Entities;

namespace GestaoLoja.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ModoEntrega> ModosEntrega { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<DetalheEncomenda> DetalhesEncomenda { get; set; }
    }
}
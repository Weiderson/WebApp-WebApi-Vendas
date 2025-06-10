using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Model;
using WebApp.Model;
using WebApp.Models;

namespace WebApp.Data
{
    public partial class AppContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Vendedor> Vendedores { get; set; }

        public virtual DbSet<Venda> Vendas { get; set; }

        public virtual DbSet<Foto> Fotos { get; set; }

        public virtual DbSet<Projecao> Projecoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendedor>().HasIndex(e => e.Id);

            modelBuilder.Entity<Venda>().HasIndex(e => e.Id);

            modelBuilder.Entity<Foto>().HasIndex(e => e.Id);

            modelBuilder.Entity<Projecao>().HasIndex(e => e.Id);
        }
    }
}
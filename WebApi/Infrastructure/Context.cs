using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Model;

namespace WebApi.infrastructure
{
    public partial class Context : DbContext
    {

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.Id);
            });


            modelBuilder.Entity<Venda>(entity =>
            {
                entity.HasKey(e => e.Id);

            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.HasKey(e => e.Id);

            });

            modelBuilder.Entity<Projecao>(entity =>
            {
                entity.HasKey(e => e.Id);

            });                   

            OnModelCreatingPartial(modelBuilder);
        }
        public virtual DbSet<Vendedor> Vendedores { get; set; }
        public virtual DbSet<Venda> Vendas { get; set; }
        public virtual DbSet<Foto> Fotos { get; set; }
        public virtual DbSet<Projecao> Projecoes { get; set; }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}

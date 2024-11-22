using KCIAOGS24.NET.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KCIAOGS24.NET.Infraestructure.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<EnderecoEntity> Endereco { get; set; }
        public DbSet<EnergiaEolicaEntity> EnergiaEolica { get; set; }
        public DbSet<EnergiaSolarEntity> EnergiaSolar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnderecoEntity>()
                .HasOne(e => e.Usuario)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(e => e.fk_usuario);

            modelBuilder.Entity<EnderecoEntity>()
                .HasOne(e => e.EnergiaSolar)
                .WithOne(c => c.Endereco)
                .HasForeignKey<EnergiaSolarEntity>(c => c.fk_endereco);

            modelBuilder.Entity<EnderecoEntity>()
                .HasOne(e => e.EnergiaEolica)
                .WithOne(c => c.Endereco)
                .HasForeignKey<EnergiaEolicaEntity>(c => c.fk_endereco);
        }
    }
}

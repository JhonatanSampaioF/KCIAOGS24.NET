using KCIAOGS24.NET.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KCIAOGS24.NET.AppData
{
    public class ApplicationContext :DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<EnderecoEntity> Endereco { get; set; }
        public DbSet<EnergiaEolicaEntity> EnergiaEolica { get; set; }
        public DbSet<EnergiaSolarEntity> EnergiaSolar { get; set; }
    }
}

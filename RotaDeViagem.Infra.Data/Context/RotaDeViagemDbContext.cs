using Microsoft.EntityFrameworkCore;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Infra.Data.EntityConfig;

namespace RotaDeViagem.Infra.Data.Context
{
    public class RotaDeViagemDbContext : DbContext
    {
        public RotaDeViagemDbContext(DbContextOptions<RotaDeViagemDbContext> options) : base(options)
        {

        }

        public DbSet<Rota> Rotas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RotaConfiguration());
        }
    }
}


using Microsoft.EntityFrameworkCore;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Infra.Data.EntityConfig;

namespace RotaDeViagem.Infra.Data.Context
{
    public class RotaDeViagemDbContext : DbContext
    {
        public RotaDeViagemDbContext()
        {
        }

        public RotaDeViagemDbContext(DbContextOptions<RotaDeViagemDbContext> options) : base(options)
        {

        }

        public DbSet<Rota> Rotas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RotaMapping());
            modelBuilder.Entity<Rota>(entity =>
            {
                entity.Property(e => e.Origem)
                      .HasConversion(
                          v => v.ToUpper(), // ao salvar
                          v => v.ToUpper()  // ao ler
                      );

                entity.Property(e => e.Destino)
                      .HasConversion(
                          v => v.ToUpper(),
                          v => v.ToUpper()
                      );
            });

            modelBuilder.Entity<Rota>().HasData(
                new Rota
                {
                    Id = Guid.NewGuid(), Origem = "GRU", Destino = "BRC", Valor = 10
                },
                new Rota
                {
                    Id = Guid.NewGuid(), Origem = "BRC", Destino = "SCL", Valor = 10
                },
                new Rota
                {
                    Id = Guid.NewGuid(), Origem = "GRU", Destino = "CDG", Valor = 75
                },
                new Rota
                {
                    Id = Guid.NewGuid(), Origem = "GRU", Destino = "SCL", Valor = 75
                },
                new Rota
                {
                    Id = Guid.NewGuid(), Origem = "GRU", Destino = "ORL", Valor = 56
                },
                new Rota
                {
                    Id = Guid.NewGuid(), Origem = "ORL", Destino = "CDG", Valor = 5
                },
                new Rota
                {
                    Id = Guid.NewGuid(), Origem = "SCL", Destino = "ORL", Valor = 20
                }
            );
        }
    }
}


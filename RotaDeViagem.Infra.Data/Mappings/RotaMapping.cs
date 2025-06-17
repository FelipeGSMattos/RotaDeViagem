using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RotaDeViagem.Domain.Entities;

namespace RotaDeViagem.Infra.Data.EntityConfig
{
    public class RotaMapping : IEntityTypeConfiguration<Rota>
    {
        public RotaMapping() 
        { 

        }

        public void Configure(EntityTypeBuilder<Rota> builder)
        {
            builder.ToTable("Rotas");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Origem)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Destino)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Valor)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18,2);
        }
    }
}

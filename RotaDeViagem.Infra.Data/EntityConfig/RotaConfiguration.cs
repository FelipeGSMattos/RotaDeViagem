using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RotaDeViagem.Domain.Entities;

namespace RotaDeViagem.Infra.Data.EntityConfig
{
    public class RotaConfiguration : IEntityTypeConfiguration<Rota>
    {
        public RotaConfiguration() 
        { 

        }

        public void Configure(EntityTypeBuilder<Rota> builder)
        {
            builder.ToTable("Rotas");

            builder.HasKey(r => r.RotaId);

            builder.Property(r => r.RotaId)
                .HasColumnName("RotaId")
                .ValueGeneratedOnAdd();

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

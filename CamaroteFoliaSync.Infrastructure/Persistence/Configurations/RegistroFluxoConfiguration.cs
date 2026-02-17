using CamaroteFoliaSync.Domain.Entities;
using CamaroteFoliaSync.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CamaroteFoliaSync.Infrastructure.Persistence.Configurations;

public class RegistroFluxoConfiguration : IEntityTypeConfiguration<RegistroFluxo>
{
    public void Configure(EntityTypeBuilder<RegistroFluxo> builder)
    {
        builder.ToTable("RegistrosFluxo");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.PulseiraId).HasConversion(pulseiraId => pulseiraId.Valor,
            valor => new PulseiraId(valor)).HasMaxLength(50).IsRequired();

        builder.Property(r => r.PulseiraId).HasConversion(pulseiraId => pulseiraId.Valor,
            valor => new PulseiraId(valor)).HasMaxLength(50).IsRequired();

        builder.Property(r => r.Tipo).IsRequired();

        builder.Property(r => r.DataHora).IsRequired();

        builder.HasIndex(r => r.CamaroteId);
        builder.HasIndex(r => r.PulseiraId);
        builder.HasIndex(r => r.DataHora);
    }
}
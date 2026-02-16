using CamaroteFoliaSync.Domain.Entities;
using CamaroteFoliaSync.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CamaroteFoliaSync.Infrastructure.Persistence.Configurations;

public class FoliaoConfiguration : IEntityTypeConfiguration<Foliao>
{
    public void Configure(EntityTypeBuilder<Foliao> builder)
    {
        builder.ToTable("Folioes");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id).HasConversion(pulseiraId => pulseiraId.Valor, valor => new PulseiraId(valor))
            .HasMaxLength(50);
     ;

        builder.Property(f => f.DataRegistro).IsRequired();
    }
}
using CamaroteFoliaSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CamaroteFoliaSync.Infrastructure.Persistence.Configurations;

public class CamaroteConfiguration : IEntityTypeConfiguration<Camarote>
{
    public void Configure(EntityTypeBuilder<Camarote> builder)
    {
        builder.ToTable("Camarote");

        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);

        builder.Property(c => c.CapacidadeMaxima).IsRequired();
    }
}
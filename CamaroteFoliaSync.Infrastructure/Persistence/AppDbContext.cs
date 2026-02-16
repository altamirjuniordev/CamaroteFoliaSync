using CamaroteFoliaSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CamaroteFoliaSync.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Camarote> Camarotes => Set<Camarote>();
    public DbSet<Foliao> Folioes => Set<Foliao>();
    public DbSet<RegistroFluxo> RegistrosFluxos => Set<RegistroFluxo>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
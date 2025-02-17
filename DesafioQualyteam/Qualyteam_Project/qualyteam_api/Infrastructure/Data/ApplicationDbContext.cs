using Microsoft.EntityFrameworkCore;
using qualyteam_api.Domain.Entities;

namespace qualyteam_api.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Indicator> Indicators { get; set; }
    public DbSet<Collection> Collections { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Indicator>()
            .HasMany(i => i.Collections)
            .WithOne(c => c.Indicator)
            .HasForeignKey(c => c.IndicatorId);

        base.OnModelCreating(modelBuilder);
    }
}
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class ApplicationDbContext : DbContext
{
    public DbSet<Indicador> Indicadores { get; set; }
    public DbSet<Coleta> Coletas { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}
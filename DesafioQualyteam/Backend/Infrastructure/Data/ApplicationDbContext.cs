// Arquivo: Backend/Infrastructure/Data/ApplicationDbContext.cs

using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Indicador> Indicadores { get; set; }
        public DbSet<Coleta> Coletas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais via Fluent API, se necessário
        }
    }
}

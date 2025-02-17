using Microsoft.EntityFrameworkCore;
using qualyteam_api.Domain.Entities;

namespace qualyteam_api.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Construtor que aceita DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet para os indicadores
        public DbSet<Indicator> Indicators { get; set; }
    }
}
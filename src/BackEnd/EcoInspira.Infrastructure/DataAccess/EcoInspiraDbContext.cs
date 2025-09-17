using EcoInspira.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace EcoInspira.Infrastructure.DataAccess
{
    public class EcoInspiraDbContext : DbContext
    {
        public EcoInspiraDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcoInspiraDbContext).Assembly);
        }
    }
}

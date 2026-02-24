using Microsoft.EntityFrameworkCore;
using Migdal.Garages.Api.Models;

namespace Migdal.Garages.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<FavoriteGarage> FavoriteGarages => Set<FavoriteGarage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteGarage>()
                .HasIndex(f => f.ExternalGarageId)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}

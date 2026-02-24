using Microsoft.EntityFrameworkCore;
using Migdal.Garages.Api.Data;
using Migdal.Garages.Api.Models;

namespace Migdal.Garages.Api.Repositories
{
    public class FavoriteGaragesRepository(AppDbContext context) : IFavoriteGaragesRepository
    {
        public async Task<List<FavoriteGarage>> GetAllAsync()
        {
            return await context.FavoriteGarages
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
        }

        public async Task AddRangeAsync(List<FavoriteGarage> garages)
        {
            await context.FavoriteGarages.AddRangeAsync(garages);
        }

        public async Task RemoveAsync(string externalGarageId)
        {
            var entity = await context.FavoriteGarages
                .FirstOrDefaultAsync(g => g.ExternalGarageId == externalGarageId);

            if (entity != null)
            {
                context.FavoriteGarages.Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(string externalGarageId)
        {
            return await context.FavoriteGarages
                .AnyAsync(g => g.ExternalGarageId == externalGarageId);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

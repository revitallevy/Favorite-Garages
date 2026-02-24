using Migdal.Garages.Api.Models;

namespace Migdal.Garages.Api.Repositories
{
    public interface IFavoriteGaragesRepository
    {
        Task<List<FavoriteGarage>> GetAllAsync();

        Task AddRangeAsync(List<FavoriteGarage> garages);

        Task RemoveAsync(string externalGarageId);

        Task<bool> ExistsAsync(string externalGarageId);

        Task SaveChangesAsync();
    }
}

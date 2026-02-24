using Migdal.Garages.Api.DTOs;
using Migdal.Garages.Api.Models;

namespace Migdal.Garages.Api.Services
{
    public interface IFavoriteGaragesService
    {
        Task<List<FavoriteGarage>> GetAllAsync();

        Task AddAsync(List<AddFavoriteGarageDto> garages);

        Task RemoveAsync(string externalGarageId);
    }
}

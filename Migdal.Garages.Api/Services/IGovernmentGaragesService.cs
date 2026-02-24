using Migdal.Garages.Api.DTOs;

namespace Migdal.Garages.Api.Services
{
    public interface IGovernmentGaragesService
    {
        Task<List<GovernmentGarageDto>> GetGaragesAsync(int limit = 50);
    }
}

using Migdal.Garages.Api.DTOs;
using Migdal.Garages.Api.Models;
using Migdal.Garages.Api.Repositories;

namespace Migdal.Garages.Api.Services
{
    public class FavoriteGaragesService(IFavoriteGaragesRepository garagesRepository) : IFavoriteGaragesService
    {
        public async Task AddAsync(List<AddFavoriteGarageDto> garages)
        {
            var uniqueGarages = garages
                .GroupBy(g => g.ExternalGarageId)
                .Select(g => g.First())
                .ToList();


            var garagesList = new List<FavoriteGarage>();

            foreach (var garage in uniqueGarages)
            {
                var exists = await garagesRepository.ExistsAsync(garage.ExternalGarageId);

                if (!exists)
                {
                    garagesList.Add(new FavoriteGarage
                    {
                        ExternalGarageId = garage.ExternalGarageId,
                        Name = garage.Name,
                        Address = garage.Address,
                        Profession = garage.Profession,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            if (garagesList.Any())
            {
                await garagesRepository.AddRangeAsync(garagesList);
                await garagesRepository.SaveChangesAsync();
            }
        }

        public async Task<List<FavoriteGarage>> GetAllAsync()
        {
            return await garagesRepository.GetAllAsync();

        }

        public async Task RemoveAsync(string externalGarageId)
        {
            await garagesRepository.RemoveAsync(externalGarageId);
            await garagesRepository.SaveChangesAsync();
        }
    }
}

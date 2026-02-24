using Migdal.Garages.Api.DTOs;
using System.Text.Json;

namespace Migdal.Garages.Api.Services
{
    public class GovernmentGaragesService(HttpClient httpClient) : IGovernmentGaragesService
    {
        private const string ResourceId = "bb68386a-a331-4bbc-b668-bba2766d517d";
        public async Task<List<GovernmentGarageDto>> GetGaragesAsync(int limit = 50)
        {
            var url =
            $"https://data.gov.il/api/3/action/datastore_search?resource_id={ResourceId}&limit={limit}";

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch garages from government API.");

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var apiResponse = JsonSerializer.Deserialize<GovernmentApiResponse>(content, options);

            if (apiResponse?.Result?.Records == null)
                return new List<GovernmentGarageDto>();

            return apiResponse.Result.Records
            .Select(r => new GovernmentGarageDto
            {
                ExternalGarageId = r.ExternalGarageId.ToString(),
                Name = r.Name,
                Address = r.Address,
                Profession = r.Profession
            })
            .ToList();
        }
    }
}

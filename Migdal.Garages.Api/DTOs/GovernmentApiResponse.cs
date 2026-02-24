using System.Text.Json;
using System.Text.Json.Serialization;

namespace Migdal.Garages.Api.DTOs
{
    public class GovernmentApiResponse
    {
        public GovernmentResult Result { get; set; } = default!;
    }

    public class GovernmentResult
    {
        public List<GovernmentRecord> Records { get; set; } = new();
    }

    public class GovernmentRecord
    {
        [JsonPropertyName("mispar_mosah")]
        public int ExternalGarageId { get; set; } = default!;

        [JsonPropertyName("shem_mosah")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("ktovet")]
        public string Address { get; set; } = default!;

        [JsonPropertyName("miktzoa")]
        public string Profession { get; set; } = default!;
    }
}

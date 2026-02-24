namespace Migdal.Garages.Api.Models
{
    public class FavoriteGarage
    {
        public int Id { get; set; }

        public string ExternalGarageId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string Profession { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
    }
}

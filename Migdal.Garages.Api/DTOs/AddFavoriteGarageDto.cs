using System.ComponentModel.DataAnnotations;

namespace Migdal.Garages.Api.DTOs
{
    public class AddFavoriteGarageDto
    {
        [Required]
        public string ExternalGarageId { get; set; } = default!;

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public string Profession { get; set; } = default!;
    }
}

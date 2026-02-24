using Microsoft.AspNetCore.Mvc;
using Migdal.Garages.Api.DTOs;
using Migdal.Garages.Api.Models;
using Migdal.Garages.Api.Services;

namespace Migdal.Garages.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController(IFavoriteGaragesService favoriteService) : ControllerBase
    {
        /// <summary>
        /// Retrieves all favorite garages stored in the database.
        /// </summary>
        /// <returns>List of favorite garages.</returns>
        /// <response code="200">Returns the list of favorite garages.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<FavoriteGarage>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FavoriteGarage>>> Get()
        {
            var favorites = await favoriteService.GetAllAsync();
            return Ok(favorites);
        }

        /// <summary>
        /// Adds one or more garages to the favorites list.
        /// Duplicate garages will be ignored.
        /// </summary>
        /// <param name="garages">List of garages to add.</param>
        /// <response code="200">Garages added successfully.</response>
        /// <response code="400">Invalid request payload.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] List<AddFavoriteGarageDto> garages)
        {
            await favoriteService.AddAsync(garages);
            return Ok();
        }

        /// <summary>
        /// Removes a garage from the favorites list by its external identifier.
        /// </summary>
        /// <param name="externalGarageId">The external garage identifier.</param>
        /// <response code="204">Garage removed successfully.</response>
        /// <response code="404">Garage not found.</response>
        [HttpDelete("{externalGarageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Remove(string externalGarageId)
        {
            await favoriteService.RemoveAsync(externalGarageId);
            return NoContent();
        }
    }
}

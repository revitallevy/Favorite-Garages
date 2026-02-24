using Microsoft.AspNetCore.Mvc;
using Migdal.Garages.Api.DTOs;
using Migdal.Garages.Api.Services;

namespace Migdal.Garages.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GaragesController(IGovernmentGaragesService governmentService) : ControllerBase
    {
        /// <summary>
        /// Retrieves a list of garages from the Israeli government open data API.
        /// </summary>
        /// <param name="limit">Optional number of records to retrieve. Default is 50.</param>
        /// <returns>List of garages.</returns>
        /// <response code="200">Returns the list of garages.</response>
        /// <response code="500">If an error occurs while fetching data from the external API.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GovernmentGarageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<GovernmentGarageDto>>> Get([FromQuery] int limit = 50)
        {
            try
            {
                var garages = await governmentService.GetGaragesAsync(limit);
                return Ok(garages);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving garages.");
            }
        }
    }
}

using HotelListing.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("2.0", Deprecated =true)] // indicating deprecated versions
    // route that can be used
    //[Route("api/[controller]/[action]")] // route 1.
    //[Route("api/country")] // route 2.
    //[Route("api/{v:apiversion}/countries")] // route 3.
    [Route("api/countries")] // route 4.
    [ApiController]
    public class CountryV2Controller : ControllerBase
    {
        private DatabaseContext _context;

        public CountryV2Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(_context.Countries);
        }
    }
}
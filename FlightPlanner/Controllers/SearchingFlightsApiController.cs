using FlightPlanner.Storage;
using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class SearchingFlightsApiController : Controller
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            var airport = FlightStorage.FindAirports(search);
            return Ok(airport);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlight flight)
        {
            if (!FlightValidations.IsValidFormat(flight))
            {
                return BadRequest();
            }
            return Ok(FlightStorage.SearchFlights(flight));
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlights(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }
    }
}
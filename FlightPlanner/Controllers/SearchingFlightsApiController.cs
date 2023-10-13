using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class SearchingFlightsApiController : Controller
    {
        private readonly FlightPlannerDbContext _context;
        private readonly FindingNemo _finder;

        public SearchingFlightsApiController(FlightPlannerDbContext context)
        {
            _context = context;
            List<Flight> allFlights = _context.Flights
                     .Include(f => f.From)
                     .Include(f => f.To)
                     .ToList();

            _finder = new FindingNemo(allFlights);
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            var airport = _finder.FindAirports(search);
   
            return Ok(airport);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlight flight)
        {
            var flights = _context.Flights.Where(f => f.From.AirportCode == flight.From ||
                                                        f.To.AirportCode == flight.To ||
                                                        f.DepartureTime == flight.DepartureDate).ToArray();
            if (!FlightValidations.IsValidFormat(flight))
            {
                return BadRequest();
            }
            return Ok(new ResultGiven(flights));
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlights(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
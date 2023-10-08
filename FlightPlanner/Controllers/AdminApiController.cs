using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        private static readonly object _locker = new object();

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            lock (_locker)
            {
                var flight = _context.Flights.SingleOrDefault(f => f.Id == id);
                if (flight == null)
                {
                    return NotFound();
                }
                return Ok(flight);
            }

        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_locker)
            {
                if (FlightValidations.IsFlightInvalid(flight))
                {
                    return BadRequest();
                }

                var allFlights = _context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .ToList();

                if (FlightValidations.IsDuplicated(flight, allFlights))
                {
                    return Conflict();
                }

                _context.Flights.Add(flight);
                _context.SaveChanges();
                return Created("", flight);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult FlightDelete(int id)
        {
            lock (_locker)
            {
                var flight = _context.Flights.SingleOrDefault(f => f.Id == id);

                if (flight != null)
                {
                    _context.Flights.Remove(flight);
                }

                _context.SaveChanges();

                return Ok();
            }
        }
    }
}

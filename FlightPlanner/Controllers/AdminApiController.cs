using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightStorage _storage;

        public AdminApiController()
        {
            _storage = new FlightStorage();
        }

        private static readonly object _locker = new object();

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            lock (_locker)
            {
                var flight = FlightStorage.GetFlight(id);
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

                if (FlightValidations.IsDuplicated(flight, FlightStorage._flightStorage))
                {
                    return Conflict();
                }

                _storage.AddFlight(flight);
                return Created("", flight);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult FlightDelete(int id)
        {
            lock (_locker)
            {
                FlightStorage.FlightDelete(id);
                return Ok();
            }
        }
    }
}

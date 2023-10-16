using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IEnumerable<ISearchFlightRequest> _flightsRequestValidators;
        private readonly IMapper _mapper;

        public CustomerApiController(IFlightService flightService, IEnumerable<ISearchFlightRequest> flightsRequestValidators, IMapper mapper)
        {
            _flightService = flightService;
            _flightsRequestValidators = flightsRequestValidators;
            _mapper = mapper;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            var airport = _flightService.SearchAirport(search);

            var response = airport.Select(a => _mapper.Map<AirportRequest>(a));

            return Ok(response);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult InvalidFlightRequest(SearchFlightRequest request)
        {
            if (!_flightsRequestValidators.All(f => f.IsValid(request)))
            {
                return BadRequest();
            }

            var result = _flightService.SearchFlight(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlightsID(int id)
        {
            var flight = _flightService.GetCompleteFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FlightRequest>(flight);

            return Ok(response);
        }
    }
}
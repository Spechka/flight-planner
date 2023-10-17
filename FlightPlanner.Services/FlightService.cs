using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }
        public Flight GetCompleteFlightById(int id)
        {
            return _context.Flights.Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            return _context.Flights.Any(f => f.ArrivalTime == flight.ArrivalTime &&
                                       f.DepartureTime == flight.DepartureTime &&
                                       f.Carrier == flight.Carrier &&
                                       f.From.AirportName == flight.From.AirportName &&
                                       f.To.AirportName == flight.To.AirportName);
        }

        public void ClearAll()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }

        public List<Airport> SearchAirport(string phrase)
        {
            phrase = phrase.ToLower().Trim();
            var airports = _context.Airports
                .Where(a => a.AirportName.ToLower().Trim().Contains(phrase)
                            || a.Country.ToLower().Trim().Contains(phrase)
                            || a.City.ToLower().Trim().Contains(phrase)).ToList();

            return airports.ToList();
        }

        public PageResult SearchFlight(SearchFlightRequest request)
        {
            var flights = _context.Flights.Where(f => f.From.AirportName == request.From
                                                      || f.To.AirportName == request.To
                                                      || f.DepartureTime == request.DepartureDate).ToArray();

            var result = new PageResult(flights);
            return result;
        }
    }
}
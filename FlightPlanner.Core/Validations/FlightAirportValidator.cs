using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations
{
    public class FlightAirportValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            if (flight?.From != null && flight?.To != null)
            {
                return flight.From.AirportName?.Trim().ToLower() !=
                       flight.To.AirportName?.Trim().ToLower();
            }

            return false;
        }
    }
}

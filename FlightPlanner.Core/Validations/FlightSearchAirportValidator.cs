using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations
{
    public class FlightSearchAirportValidator : ISearchFlightRequest
    {
        public bool IsValid(SearchFlightRequest request)
        {
            if (!string.IsNullOrEmpty(request.From) && !string.IsNullOrEmpty(request.To))
            {
                return request.From.Trim().ToLower() != request.To.Trim().ToLower();
            }

            return false;
        }
    }
}
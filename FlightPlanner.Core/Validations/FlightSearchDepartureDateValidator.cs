using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations
{
    public class FlightSearchDepartureDateValidator : ISearchFlightRequest
    {
        public bool IsValid(SearchFlightRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureDate);
        }
    }
}
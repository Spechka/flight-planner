using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations
{
    public interface ISearchFlightRequest
    {
        bool IsValid(SearchFlightRequest request);
    }
}
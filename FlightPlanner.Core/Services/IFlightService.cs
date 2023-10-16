using FlightPlanner.Core.Models;
using FlightPlanner.Models;
using System.Collections.Generic;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetCompleteFlightById(int id);
        bool Exists(Flight flight);
        void ClearAll();
        List<Airport> SearchAirport(string phrase);

        PageResult SearchFlight(SearchFlightRequest request);
    }
}
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlightPlanner.Storage
{
    public class FindingNemo
    {
        public List<Flight> _allFlights;

        public FindingNemo(List<Flight> allFlights) {
            _allFlights = allFlights;
        }

        public Airport[] FindAirports(string phrase)
        {
            phrase = phrase.ToLower().Trim();
            var fromAirports = _allFlights.Where(f => f.From.AirportCode.ToLower().Trim().Contains(phrase)
                                                   || f.From.City.ToLower().Trim().Contains(phrase)
                                                   || f.From.Country.ToLower().Trim().Contains(phrase))
                                          .Select(a => a.From).ToArray();

            var toAirports = _allFlights.Where(f => f.To.AirportCode.ToLower().Trim().Contains(phrase)
                                                 || f.To.City.ToLower().Trim().Contains(phrase)
                                                 || f.To.Country.ToLower().Trim().Contains(phrase))
                                        .Select(f => f.To).ToArray();

            return fromAirports.Concat(toAirports).ToArray();
        }

        public ResultGiven SearchFlights(SearchFlight req)
        {
            return new ResultGiven(_allFlights.ToArray());
        }
    }
}

using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        private static readonly object _lock = new object();

        public static List<Flight> _flightStorage = new List<Flight>();

        private static int _id = 1;

        public void AddFlight(Flight flight) 
        {
            flight.Id = _id++;
            _flightStorage.Add(flight);
        }

        public static Flight GetFlight(int id)
        {
            return _flightStorage.FirstOrDefault(f => f.Id == id);
        }

        public static void Clear()
        {
            _flightStorage.Clear();
            _id = 0;
        }

        public static void FlightDelete(int id)
        {
            var flight = GetFlight(id);

            if (flight != null)
            {
                _flightStorage.Remove(flight);
            }
        }

        public static Airport[] FindAirports(string phrase)
        {
            {
                phrase = phrase.ToLower().Trim();
                var fromAirports = _flightStorage.Where(f => f.From.AirportCode.ToLower().Trim().Contains(phrase)
                                                       || f.From.City.ToLower().Trim().Contains(phrase)
                                                       || f.From.Country.ToLower().Trim().Contains(phrase))
                    .Select(a => a.From).ToArray();
                var toAirports = _flightStorage.Where(f => f.To.AirportCode.ToLower().Trim().Contains(phrase)
                                                     || f.To.City.ToLower().Trim().Contains(phrase)
                                                     || f.To.Country.ToLower().Trim().Contains(phrase))
                    .Select(f => f.To).ToArray();

                return fromAirports.Concat(toAirports).ToArray();
            }
        }

        public static ResultGiven SearchFlights(SearchFlight req)
        {
            return new ResultGiven(_flightStorage.ToArray());
        }
    }
}

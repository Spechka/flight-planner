using FlightPlanner.Models;
using System.Collections;
using System.Text.Json.Serialization;

namespace FlightPlanner.Validations
{
    public static class FlightValidations
    {

        public static bool IsFlightInvalid(Flight flight) 
        {
            return HasWrongValues(flight) || HasDuplicatedAirports(flight) || IsStrangeDate(flight);
        }

        public static bool IsDuplicated (Flight flight, List<Flight> list)
        {
            return list.Any(inStorageFlight =>
                inStorageFlight.From.Country == flight.From.Country
                && inStorageFlight.From.City == flight.From.City
                && inStorageFlight.From.AirportCode == flight.From.AirportCode
                && inStorageFlight.To.Country == flight.To.Country
                && inStorageFlight.To.City == flight.To.City
                && inStorageFlight.To.AirportCode == flight.To.AirportCode
                && inStorageFlight.Carrier == flight.Carrier
                && inStorageFlight.DepartureTime == flight.DepartureTime
                && inStorageFlight.ArrivalTime == flight.ArrivalTime
             );
        }

        public static bool HasWrongValues(Flight flight)
        {
            return flight == null ||
                flight.From == null ||
                flight.To == null ||
                String.IsNullOrEmpty(flight.From.Country) ||
                String.IsNullOrEmpty(flight.From.City) ||
                String.IsNullOrEmpty(flight.From.AirportCode) ||
                String.IsNullOrEmpty(flight.To.Country) ||
                String.IsNullOrEmpty(flight.To.City) ||
                String.IsNullOrEmpty(flight.To.AirportCode) ||
                String.IsNullOrEmpty(flight.Carrier) ||
                String.IsNullOrEmpty(flight.DepartureTime) ||
                String.IsNullOrEmpty(flight.ArrivalTime);
        }

        public static bool HasDuplicatedAirports(Flight flight)
        {
            return flight.From.Country.ToLower().Trim() == flight.To.Country.ToLower().Trim() &&
                flight.From.City.ToLower().Trim() == flight.To.City.ToLower().Trim() &&
                flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim();
        }

        public static bool IsStrangeDate(Flight flight)
        {
            return DateTime.Parse(flight.DepartureTime) >= DateTime.Parse(flight.ArrivalTime);
        }

        public static bool IsValidFormat(SearchFlight request)
        {
            if (request.From == request.To)
            {
                return false;
            }

            if (request.To == null || request.From == null || request.DepartureDate == null)
            {
                return false;
            }

            return true;
        }
    }
}

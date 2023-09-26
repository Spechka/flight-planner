using System.Text.Json.Serialization;

namespace FlightPlanner.Models
{
    public class Airport
    {
        public string Country { get; set; }
        public string City { get; set; }

        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }

        public bool Equals(Airport airport)
        {
            var CountryCheck = this.Country == airport.Country;
            var CityCheck = this.City == airport.City;
            var AirportNameCheck = this.AirportCode == airport.AirportCode;
            return CountryCheck && CityCheck && AirportNameCheck;
        }
    }
}
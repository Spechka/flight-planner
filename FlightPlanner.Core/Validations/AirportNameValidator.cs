using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations
{
    public class AirportNameValidator : IAirportValidator
    {
        public bool IsValid(Airport airport)
        {
            return !string.IsNullOrEmpty(airport?.AirportName);
        }
    }
}

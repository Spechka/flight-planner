namespace FlightPlanner.Models
{
    public class Flight
    {
        
        public int Id { get; set; }
        
        public Airport From { get; set; }

        public Airport To { get; set; }

        public string Carrier { get; set; }

        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }

        public bool Equals(Flight flight)
        {
            var carrierCheck = this.Carrier == flight.Carrier;
            var FromCheck = this.From.Equals(flight.From);
            var ToCheck = this.To.Equals(flight.To);
            var DepartureCheck = this.DepartureTime == flight.DepartureTime;
            var ArrivalCheck = this.ArrivalTime == flight.ArrivalTime;
            return carrierCheck && FromCheck && ToCheck && DepartureCheck && ArrivalCheck;
        }
    }
}

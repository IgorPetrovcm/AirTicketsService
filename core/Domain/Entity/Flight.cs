using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Flight
    {
        [Description("flight_id")]
        public int FlightId { get; set; }

        [Description("flight_no")]
        public string FlightNo { get; set; }

        [Description("scheduled_departure")]
        public DateTime ScheduledDeparture { get; set; }

        [Description("scheduled_arrival")]
        public DateTime ScheduledArrival { get; set; }

        [Description("departure_airport")]
        public string DepartureAirport { get; set; }

        [Description("arrival_airport")]
        public string ArrivalAirport { get; set; }

        [Description("status")]
        public string Status { get; set; }

        [Description("aircraft_code")]
        public string AircraftCode { get; set; }

        [Description("actual_departure")]
        public DateTime? ActualDeparture { get; set; }

        [Description("actual_arrival")]
        public DateTime? ActualArrival { get; set; }
    }
}
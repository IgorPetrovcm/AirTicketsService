using System.ComponentModel;

namespace Domain.Entity
{
    public class TicketFlight
    {
        [Description("ticket_no")]
        public string TicketNo { get; set; }

        [Description("flight_id")]
        public int FlightId { get; set; }

        [Description("fare_conditions")]
        public string FareConditions { get; set; }

        [Description("amount")]
        public decimal Amount { get; set; }
    }
}
using System.ComponentModel;

namespace Domain.Entity
{
    public class BoardingPass
    {
        [Description("ticket_no")]
        public string TicketNo { get; set; }

        [Description("flight_id")]
        public int FlightId { get; set; }

        [Description("boarding_no")]
        public int BoardingNo { get; set; }

        [Description("seat_no")]
        public string SeatNo { get; set; }
    }
}

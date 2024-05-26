using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entity
{
    public class Seat
    {
        [Description("aircraft_code")]
        public string AircraftCode { get; set; }

        [Description("seat_no")]
        public string SeatNo { get; set; }

        [Description("fare_conditions")]
        public string FareConditions { get; set; }
    }
}
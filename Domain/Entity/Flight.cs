using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("flights", Schema = "bookings")]
public class Flight
{
     [Key]
    [Column("flight_id")]
    public int FlightId { get; set; }

    [Column("flight_no")]
    public string FlightNo { get; set; }

    [Column("scheduled_departure")]
    public DateTime ScheduledDeparture { get; set; }

    [Column("scheduled_arrival")]
    public DateTime ScheduledArrival { get; set; }

    [Column("departure_airport")]
    public string DepartureAirport { get; set; }

    [Column("arrival_airport")]
    public string ArrivalAirport { get; set; }

    [Column("status")]
    public string Status { get; set; }

    [Column("aircraft_code")]
    public string AircraftCode { get; set; }

    [Column("actual_departure")]
    public DateTime? ActualDeparture { get; set; }

    [Column("actual_arrival")]
    public DateTime? ActualArrival { get; set; }

    public ICollection<TicketFlight> TicketFlights { get; set; }

    [ForeignKey(nameof(AircraftCode))]
    public Aircraft Aircraft { get; set; }

    [ForeignKey(nameof(DepartureAirport))]
    public Airport Departure { get; set; }

    [ForeignKey(nameof(ArrivalAirport))]
    public Airport Arrival { get; set; }

}

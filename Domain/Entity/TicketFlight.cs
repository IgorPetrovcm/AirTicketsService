using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ticket_flights", Schema = "bookings")]
public class TicketFlight
{
    [Key]
    [Column("ticket_no")]
    public string TicketNo { get; set; }

    [Column("flight_id")]
    public int FlightId { get; set; }

    [Column("fare_conditions")]
    public string FareConditions { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    public ICollection<BoardingPass> BoardingPasses { get; set;}

    [ForeignKey(nameof(TicketNo))]
    public Ticket Ticket { get; set; }

    [ForeignKey(nameof(FlightId))]
    public Flight Flight { get; set; }
}
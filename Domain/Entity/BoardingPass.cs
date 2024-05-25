using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("boarding_passes", Schema = "bookings")]
public class BoardingPass
{
    [Key]
    [Column("ticket_no")]
    public string TicketNo { get; set; }

    [Column("flight_id")]
    public int FlightId { get; set; }

    [Column("boarding_no")]
    public int BoardingNo { get; set; }

    [Column("seat_no")]
    public string SeatNo { get; set; }
    
    public TicketFlight TicketFlight { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("seats", Schema = "bookings")]
public class Seat
{
    [Key]
    [Column("aircraft_code")]
    public string AircraftCode { get; set; }

    [Column("seat_no")]
    public string SeatNo { get; set; }

    [Column("fare_conditions")]
    public string FareConditions { get; set; }
    
    [ForeignKey(nameof(AircraftCode))]
    public Aircraft Aircraft { get; set; }
}
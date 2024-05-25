using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


// This table named in DB as "airports_data" 
[Table("aircrafts_data", Schema = "bookings")]
public class Aircraft
{
    [Key]
    [Column("aircraft_code")]
    public string AircraftCode { get; set; }

    [Column("model")]
    public string Model { get; set; }

    [Column("range")]
    public int Range { get; set; }

    public ICollection<Flight> Flights { get; set; }
    public ICollection<Seat> Seats { get; set; }
}
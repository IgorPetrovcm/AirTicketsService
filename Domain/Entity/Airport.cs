using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("airports_data", Schema = "bookings")]
public class Airport
{
    [Key]
    [Column("airport_code")]
    public string AirportCode { get; set; }

    [Column("airport_name")]
    public string AirportName { get; set; }

    [Column("city")]
    public string City { get; set; }

    [Column("coordinates")]
    public string Coordinates { get; set; }

    [Column("timezone")]
    public string Timezone { get; set; }
    public ICollection<Flight> DepartingFlights { get; set; }
    public ICollection<Flight> ArrivingFlights { get; set; }
}
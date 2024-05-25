using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

  [Table("tickets", Schema = "bookings")]
public class Ticket
{
    [Key]
    [Column("ticket_no")]
    public string TicketNo { get; set; }

    [Column("book_ref")]
    public string BookRef { get; set; }

    [Column("passenger_id")]
    public string PassengerId { get; set; }

    [Column("passenger_name")]
    public string PassengerName { get; set; }

    [Column("contact_data")]
    public string ContactData { get; set; }

    public ICollection<TicketFlight> TicketFlights { get; set; }

    [ForeignKey(nameof(BookRef))]
    public Booking Booking { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("bookings", Schema = "bookings")]
public class Booking
{
    [Key]
    [Column("book_ref")]
    public string BookRef { get; set; }

    [Column("book_date")]
    public DateTime BookDate { get; set; }

    [Column("total_amount")]
    public decimal TotalAmount { get; set; }

    public ICollection<Ticket> Tickets { get; set; }
}
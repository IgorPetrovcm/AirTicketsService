using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Booking
    {
        [Description("book_ref")]
        public string BookRef { get; set; }

        [Description("book_date")]
        public DateTime BookDate { get; set; }

        [Description("total_amount")]
        public decimal TotalAmount { get; set; }
    }
}

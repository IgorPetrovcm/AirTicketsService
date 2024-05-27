using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Ticket
  {
      [Description("ticket_no")]
      public string TicketNo { get; set; }

      [Description("book_ref")]
      public string BookRef { get; set; }

      [Description("passenger_id")]
      public string PassengerId { get; set; }

      [Description("passenger_name")]
      public string PassengerName { get; set; }

      // TODO: may be using another type, because this field is json 
      [Description("contact_data")]
      public string ContactData { get; set; }
  }
}
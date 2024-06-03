namespace Producer.Core.Application;

using Producer.Core.Model;

public class TicketRequestDTO 
{
    public string Destination { get; set; }

    public Ticket Ticket { get; set; }
}
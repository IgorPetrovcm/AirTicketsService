namespace DocumentFlowService.GeneralModel.DTOs;

using DocumentFlowService.GeneralModel;

public class TicketRequestDTO 
{
    public string Destination { get; set; }

    public Ticket Ticket { get; set; }
}

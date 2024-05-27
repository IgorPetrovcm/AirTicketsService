namespace Domain.DTO
{
    public class CreateBookingDto
    {
        public int FlightId { get; set; }
        public IEnumerable<CraeteTicketDto> Tickets { get; set; }
    }
}
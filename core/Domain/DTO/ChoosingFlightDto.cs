namespace Domain.DTO
{
    public class ChoosingFlightDto
    {
        
        public DateOnly ScheduledDeparture { get; set; }

        public string DepartureAirport { get; set; }

        public string ArrivalAirport { get; set; }
    }
}
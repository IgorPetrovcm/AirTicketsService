using Domain.DTO;
using Domain.Entity;

namespace Service.Interfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetAllAsync();        
        Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int flightId);
        Task<IEnumerable<Flight>> GetByDepartureDateAsync(ChoosingFlightDto choosingFlight);
        Task<IEnumerable<Ticket>> GetTicketsAsync(int flightId);
        Task<List<string>> CreateBooking(CreateBookingDto bookingDto);
    }
}
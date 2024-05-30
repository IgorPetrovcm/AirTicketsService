using Domain.DTO;
using Domain.Entity;

namespace Persistent.Interfaces
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int flightId);
        Task<IEnumerable<Flight>> GetByDateAndAirportsAsync(ChoosingFlightDto choosingFlight);
        Task<IEnumerable<Ticket>> GetTicketsAsync(int flightId);
    }
}
using Domain.Entity;

namespace Persistent.Interfaces
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int flightId);
        Task<IEnumerable<Flight>> GetByDepartureDateAsync(DateOnly dateTime);
        Task<IEnumerable<Ticket>> GetTicketsAsync(int flightId);
        Task<string> CreateBooking(DateTime dateCreated);
    }
}
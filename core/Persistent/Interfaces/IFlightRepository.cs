using Domain.Entity;

namespace Persistent.Interfaces
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int flightId);
    }
}
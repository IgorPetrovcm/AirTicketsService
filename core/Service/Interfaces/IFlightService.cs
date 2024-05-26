using Domain.Entity;

namespace Service.Interfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetAllAsync();        
        Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int flightId);
    }
}
using Domain.Entity;
using Service.Interfaces;
using Persistent.Interfaces;

namespace Service.Implementation
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository flightRepository;
        public FlightService(IFlightRepository repository) 
        {
            this.flightRepository = repository;
        }
        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await flightRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int flightId)
        {
            return await flightRepository.GetAvailableSeatsAsync(flightId);
        }
    }
}
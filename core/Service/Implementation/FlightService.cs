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

        //TODO: Perhaps the method should take all the information to create reservations, tickets and seats 
        public async Task<string> CreateBooking()
        {
            return await flightRepository.CreateBooking(DateTime.Now);
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await flightRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int flightId)
        {
            return await flightRepository.GetAvailableSeatsAsync(flightId);
        }

        public async Task<IEnumerable<Flight>> GetByDepartureDateAsync(DateOnly date)
        {
            return await flightRepository.GetByDepartureDateAsync(date);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(int flightId)
        {
           return await flightRepository.GetTicketsAsync(flightId);
        }
    }
}
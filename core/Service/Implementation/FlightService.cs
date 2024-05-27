using Domain.Entity;
using Service.Interfaces;
using Domain.DTO;
using Persistent.Interfaces;
using Persistent;

namespace Service.Implementation
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository flightRepository;
        private readonly IBookingCreator bookingCreator;
        private readonly KeyGenerator keyGenerator;
        public FlightService(IFlightRepository flightRepository, IBookingCreator bookingCreator, KeyGenerator keyGenerator) 
        {
            this.keyGenerator = keyGenerator;
            this.bookingCreator = bookingCreator;
            this.flightRepository = flightRepository;
        }

        //TODO: Perhaps the method should take all the information to create reservations, tickets and seats 
        //TODO: May be dateCreated is not needed???? Because I use it anyway DateTime.Now() )))
        public async Task<List<string>> CreateBooking(CreateBookingDto bookingDto)
        {
            string newBookingKey = await keyGenerator.GetNextPrimaryKeyAsync<Booking>();
            await bookingCreator.CreateBookingAsync(newBookingKey, DateTime.Now);

            List<string> ticketsKeys = new List<string>(bookingDto.Tickets.Count());
            foreach (CraeteTicketDto ticket in bookingDto.Tickets)
            {
                string newBoardingPassKey = await keyGenerator.GetNextPrimaryKeyAsync<BoardingPass>();
                string newTicketKey = await keyGenerator.GetNextPrimaryKeyAsync<Ticket>();
                await bookingCreator.AddTicketAsync(newBookingKey, newTicketKey, bookingDto.FlightId, int.Parse(newBoardingPassKey), ticket);
                ticketsKeys.Add(newTicketKey);
            }

            return ticketsKeys;
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
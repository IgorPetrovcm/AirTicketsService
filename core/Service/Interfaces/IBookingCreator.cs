using Domain.DTO;

namespace Service.Interfaces
{
    public interface IBookingCreator
    {
        //TODO: changed string on Class that mean the identifier of Booking
        Task CreateBookingAsync(string bookRef, DateTime dateCreated);
        Task AddTicketAsync(string bookRef, string ticketNo, int flightId, int boardingNo,  CraeteTicketDto ticket);
    }
}
using System.Data;
using Dapper;
using Domain.DTO;
using Domain.Entity;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Service.Interfaces;

namespace Persistent.Implementation
{
    public class BookingCreator : IBookingCreator
    {
        private readonly string connectionString;
        private const string FARE_CONDITION = "Economy"; 
        private const int AMOUNT = 10_000; 
        public BookingCreator(IConfiguration configuration) 
        {
            CustomMapper.SetMapping();
            this.connectionString = configuration["ConnectionStrings:Default"];
        } 

        public async Task AddTicketAsync(string bookRef, string ticketNo, int flightId, int boardingNo, CraeteTicketDto ticket)
        {
            using(IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                using (var tran = connection.BeginTransaction()) 
                { 
                    await connection.ExecuteAsync(@"insert into bookings.tickets values(@TicketNo, @BookRef, @PassId, @PassName)",
                    new {
                            BookRef = bookRef, 
                            TicketNo = ticketNo, 
                            PassId = ticket.PassengerId, 
                            PassName = ticket.PassengerName, 
                            //ContectData = ticket.ContactData
                    });

                    await connection.ExecuteAsync(@"insert into bookings.ticket_flights values(@TicketNo, @FlightId, @FareConditoin, @Amout)",
                    new {
                            TicketNo = ticketNo,
                            FlightId = flightId,
                            FareConditoin = FARE_CONDITION,
                            Amout = AMOUNT, 
                    });

                    await connection.ExecuteAsync(@"insert into bookings.boarding_passes values(@TicketNo, @FlightId, @BoardingNo, @SeatNo) ",
                    new {
                        TicketNo = ticketNo,
                        FlightId = flightId,
                        BoardingNo = boardingNo,
                        SeatNo = ticket.SeatNo
                    });
                    
                    tran.Commit(); //Or rollback 
                }
            }
        }

        public async Task CreateBookingAsync(string bookRef, DateTime dateCreated)
        {
            using(IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(@"insert into bookings.bookings values(@BookRef, @DateCreated, 0)",
                                        new {BookRef = bookRef, DateCreated = dateCreated});
            }
        }
    }
}
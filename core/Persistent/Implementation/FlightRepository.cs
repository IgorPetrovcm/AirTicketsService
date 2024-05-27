using Domain.Entity;
using Persistent.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;
using Dapper;
using System.Reflection;
using System.ComponentModel;

namespace Persistent.Implementation
{
    public class FlightRepository : IFlightRepository
    {
        private readonly string connectoinString;
        private readonly KeyGenerator keyGen;
        public FlightRepository(IConfiguration configuration) 
        {
            CustomMapper.SetMapping();
            this.connectoinString = configuration["ConnectionStrings:Default"];
            keyGen = new KeyGenerator(connectoinString);
        } 
        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            using(IDbConnection connection = new NpgsqlConnection(connectoinString))
            {
                return await connection.QueryAsync<Flight>("SELECT * FROM bookings.flights LIMIT 10");
            }
        }

        public async Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int flightId)
        {
            using(IDbConnection connection = new NpgsqlConnection(connectoinString))
            {
                return await connection
                    .QueryAsync<Seat>(@"select s.*  from  bookings.flights f 
                                        join bookings.seats s on s.aircraft_code  = f.aircraft_code 
                                        where f.flight_id = @FlightId and 
                                        s.seat_no  not in (select bp.seat_no from bookings.boarding_passes bp where bp.flight_id = f.flight_id)
                                    ", new {FlightId = flightId});
            }
        }

        public async Task<IEnumerable<Flight>> GetByDepartureDateAsync(DateOnly date)
        {
            using(IDbConnection connection = new NpgsqlConnection(connectoinString))
            {
                var dateTime = date.ToDateTime(TimeOnly.MinValue); 
                return await connection.QueryAsync<Flight>(@"
                    select * from bookings.flights f 
                    where f.scheduled_departure::date = @Date
                ", new { Date = dateTime });
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(int flightId)
        {
            using(IDbConnection connection = new NpgsqlConnection(connectoinString))
            {
                return await connection
                    .QueryAsync<Ticket>(@"SELECT t.*
                                        FROM bookings.tickets t
                                        JOIN bookings.ticket_flights tf ON t.ticket_no = tf.ticket_no
                                        WHERE tf.flight_id = @FlightId;
                                    ", new {FlightId = flightId});
            }
        }
        public async Task<string> CreateBooking(DateTime dateCreated)
        {
            string newPk = await keyGen.GetNextPrimaryKeyAsync();
            
            using(IDbConnection connection = new NpgsqlConnection(connectoinString))
            {
                await connection.ExecuteAsync(@"insert into bookings.bookings values(@Key, @Date, 0)"
                                            , new {Key = newPk, Date = dateCreated});
            }

            return newPk;
        }
    }
}
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;
using Dapper;
using Persistent.Interfaces;
using Domain.Entity;
using Domain.DTO;

namespace Persistent.Implementation
{
    public class FlightRepository : IFlightRepository
    {
        private readonly string connectionString;
        public FlightRepository(IConfiguration configuration) 
        {
            CustomMapper.SetMapping();
            this.connectionString = configuration["ConnectionStrings:Default"];
        } 
        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            using(IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryAsync<Flight>("SELECT * FROM bookings.flights LIMIT 10");
            }
        }

        public async Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int flightId)
        {
            using(IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                return await connection
                    .QueryAsync<Seat>(@"select s.*  from  bookings.flights f 
                                        join bookings.seats s on s.aircraft_code  = f.aircraft_code 
                                        where f.flight_id = @FlightId and 
                                        s.seat_no  not in (select bp.seat_no from bookings.boarding_passes bp where bp.flight_id = f.flight_id)
                                    ", new {FlightId = flightId});
            }
        }

        public async Task<IEnumerable<Flight>> GetByDateAndAirportsAsync(ChoosingFlightDto choosingFlight)
        {
            using(IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var dateTime = choosingFlight.ScheduledDeparture.ToDateTime(TimeOnly.MinValue); 
                return await connection.QueryAsync<Flight>(@"
                    select * from bookings.flights f 
                    where f.scheduled_departure::date = @Date and f.departure_airport = @DepartureAirport and f.arrival_airport = @ArrivalAirport
                ", new { Date = dateTime, choosingFlight.DepartureAirport, choosingFlight.ArrivalAirport });
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(int flightId)
        {
            using(IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                return await connection
                    .QueryAsync<Ticket>(@"SELECT t.*
                                        FROM bookings.tickets t
                                        JOIN bookings.ticket_flights tf ON t.ticket_no = tf.ticket_no
                                        WHERE tf.flight_id = @FlightId;
                                    ", new {FlightId = flightId});
            }
        }
    }
}
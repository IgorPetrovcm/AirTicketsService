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
        public FlightRepository(IConfiguration configuration) 
        {
            this.connectoinString = configuration["ConnectionStrings:Default"];

            var Flmap = new CustomPropertyTypeMap(typeof(Flight), (type, columnName)
                => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            SqlMapper.SetTypeMap(typeof(Flight), Flmap);
            var Stmap = new CustomPropertyTypeMap(typeof(Seat), (type, columnName)
                => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            SqlMapper.SetTypeMap(typeof(Seat), Stmap);
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

        static string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var attrib = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);
            return (attrib?.Description ?? member.Name).ToLower();
        }
    }
}
using System.Data;
using Dapper;
using Domain.DTO;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Service.Interfaces;

namespace Persistent.Implementation
{
    public class KeysMaxProvider : IKeysMaxProvider
    {
        private readonly string connectionString;
        public KeysMaxProvider(IConfiguration configuration) 
        {
            CustomMapper.SetMapping();
            this.connectionString = configuration["ConnectionStrings:Default"];
        } 

        public async Task<string> GetMaxKeyForBookingAsync()
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var maxKey = await connection.ExecuteScalarAsync<string>($"SELECT MAX(book_ref) FROM bookings.bookings");

                return maxKey;
            }
        }

        public async Task<string> GetMaxKeyForTicketAsync()
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var maxKey = await connection.ExecuteScalarAsync<string>($"SELECT MAX(ticket_no) FROM bookings.tickets");

                return maxKey;
            }
        }
    }
}
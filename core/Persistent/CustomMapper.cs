using System.ComponentModel;
using System.Reflection;
using Dapper;
using Domain.Entity;

namespace Persistent
{
    public static class CustomMapper
    {
        public static void SetMapping() 
        {
            var FlightMap = new CustomPropertyTypeMap(typeof(Flight), (type, columnName)
                => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            SqlMapper.SetTypeMap(typeof(Flight), FlightMap);

            var SeatMap = new CustomPropertyTypeMap(typeof(Seat), (type, columnName)
                => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            SqlMapper.SetTypeMap(typeof(Seat), SeatMap);

            var TicketMap = new CustomPropertyTypeMap(typeof(Ticket), (type, columnName)
                => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName.ToLower()));
            SqlMapper.SetTypeMap(typeof(Ticket), TicketMap);
        }

        private static string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var attrib = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);
            return (attrib?.Description ?? member.Name).ToLower();
        }
    }
}
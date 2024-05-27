namespace Domain.DTO
{
    //TODO: may be add prop FareCondition 
    public class CraeteTicketDto
    {
        public string PassengerId { get; set; }
        public string PassengerName { get; set; }
        //TODO: exeption :Npgsql.PostgresException (0x80004005): 42804: column "contact_data" is of type jsonb but expression is of type text
        public string? ContactData { get; set; } 
        public string SeatNo { get; set; }    
    }
}
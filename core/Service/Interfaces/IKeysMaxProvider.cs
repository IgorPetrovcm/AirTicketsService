namespace Service.Interfaces
{
    public interface IKeysMaxProvider
    {
        Task<string> GetMaxKeyForBookingAsync();
        Task<string> GetMaxKeyForTicketAsync();
    }
}
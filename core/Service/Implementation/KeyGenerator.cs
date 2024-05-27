using Domain.Entity;
using Service.Interfaces;

namespace Persistent
{
    public class KeyGenerator
    {
        private readonly IKeysMaxProvider keysMaxProvider;

        public KeyGenerator(IKeysMaxProvider keysMaxProvider)
        {
            this.keysMaxProvider = keysMaxProvider;
        }

        public async Task<string> GetNextPrimaryKeyAsync<T>()
        {
            string maxKey = typeof(T) 
            switch
            {
                Type t when t == typeof(Ticket) => await keysMaxProvider.GetMaxKeyForTicketAsync(),
                Type t when t == typeof(Booking) => await keysMaxProvider.GetMaxKeyForBookingAsync(),
                Type t when t == typeof(BoardingPass) => newBoardingKey(),
                _ => throw new ArgumentException("Unsupported type", nameof(T))
            };

            string nextKey = incrementBase36Key(maxKey);
            return nextKey;
        }

        private static string newBoardingKey() 
        {
            Random random = new Random();
            return random.Next(1_000, 100_000).ToString();
        }

        private string incrementBase36Key(string base36Key)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            long number = 0;

            // Конвертация 36-ричного ключа в десятичное число
            foreach (char c in base36Key)
            {
                number = number * 36 + chars.IndexOf(c);
            }

            // Увеличение числа
            number += 1;

            // Конвертация обратно в 36-ричную систему
            var newKey = string.Empty;
            while (number > 0)
            {
                newKey = chars[Convert.ToInt32(number % 36)] + newKey;
                number /= 36;
            }

            // Дополнение нулями до длины 6
            return newKey.PadLeft(base36Key.Length, '0');
        }
    }
}
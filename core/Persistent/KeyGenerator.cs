using System.Data;
using Dapper;
using Npgsql;

namespace Persistent
{
    public class KeyGenerator
    {
        private readonly string connectionString;

        public KeyGenerator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<string> GetNextPrimaryKeyAsync()
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                // Получение максимального текущего значения
                var maxKey = await connection.ExecuteScalarAsync<string>("SELECT MAX(book_ref) FROM bookings.bookings");
                if (string.IsNullOrEmpty(maxKey))
                {
                    return "000000"; // Начальное значение
                }

                // Генерация следующего значения
                var nextKey = IncrementBase36Key(maxKey);
                return nextKey;
            }
        }

        private string IncrementBase36Key(string base36Key)
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
            return newKey.PadLeft(6, '0');
        }
    }
}
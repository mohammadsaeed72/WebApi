using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace SampleWebApi._0.Helpers
{
    public static class DistributedCacheExtentions
    {

        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? UnusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = UnusedExpireTime;


            var jsonDate = JsonSerializer.Serialize(data);

            try
            {
                await cache.SetStringAsync(recordId, jsonDate, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DistributedCacheExtentions SetRecordAsync Error{ex.Message}");
            }
        }

        public static void SetRecord<T>(this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? UnusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = UnusedExpireTime;


            var jsonDate = JsonSerializer.Serialize(data);

            try
            {
                cache.SetString(recordId, jsonDate, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DistributedCacheExtentions SetRecord Error{ex.Message}");
            }
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            try
            {
                var jsonData = await cache.GetStringAsync(recordId);


                if (string.IsNullOrEmpty(jsonData))
                {
                    return default(T);
                }

                T returnVal = JsonSerializer.Deserialize<T>(jsonData);
                return returnVal;
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }

        public static async Task DeleteRecordAsync(this IDistributedCache cache, string recordId)
        {
            await cache.RemoveAsync(recordId);
        }

        public static T GetRecord<T>(this IDistributedCache cache, string recordId)
        {
            try
            {
                var jsonData = cache.GetString(recordId);


                if (string.IsNullOrEmpty(jsonData))
                {
                    return default(T);
                }

                T returnVal = JsonSerializer.Deserialize<T>(jsonData);
                return returnVal;
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }
    }
}

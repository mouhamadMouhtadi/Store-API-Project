using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Store.Service.Services.CachService;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Store.Service.Services.CachService
{
    public class CachService : ICachService
    {
        private readonly IDatabase _database;
        public CachService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<string> GetCacheResponseAsync(string key)
        {
            var cachedResponse = await _database.StringGetAsync(key);
            if (cachedResponse.IsNullOrEmpty)
                return null;
            return cachedResponse.ToString();
        }

        public async Task SetCacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;
            var Options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var serializeResponse = JsonSerializer.Serialize(response, Options);   
            await _database.StringSetAsync(key,serializeResponse, timeToLive);
        }
    }
}

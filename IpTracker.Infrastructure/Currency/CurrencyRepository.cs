using IpTracker.Domain.Modules.Currency;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Infrastructure.Currency
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private const string REPOKEY = "Currency-";

        public CurrencyRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public void Add(Domain.Modules.Currency.Currency currency)
        {
            var redisDb = _connectionMultiplexer.GetDatabase();
            redisDb.StringSetAsync(REPOKEY + currency.CurrencyCode, JsonConvert.SerializeObject(currency));
        }

        public async Task<Domain.Modules.Currency.Currency> GetAsync(string currencyCode)
        {
            var redisDb = _connectionMultiplexer.GetDatabase();
            var respRedis = await redisDb.StringGetAsync(REPOKEY + currencyCode);
            if (respRedis.IsNull)
                return null;
            else {
                var respFinal = JsonConvert.DeserializeObject<Domain.Modules.Currency.Currency>(respRedis);
                return await Task.FromResult(respFinal);
            }
        }
    }
}

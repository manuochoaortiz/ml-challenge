using IpTracker.Domain.Modules.Country;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Infrastructure.Country
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private const string REPOKEY = "Country-";

        public CountryRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public void Add(Domain.Modules.Country.Country country)
        {
            var redisDb = _connectionMultiplexer.GetDatabase();
            redisDb.StringSetAsync(REPOKEY + country.Ip, JsonConvert.SerializeObject(country));
        }

        public async Task<Domain.Modules.Country.Country> GetAsync(string ip)
        {
            var redisDb = _connectionMultiplexer.GetDatabase();
            var respRedis = await redisDb.StringGetAsync(REPOKEY + ip);
            if (respRedis.IsNull)
                return null;
            else
            {
                var respFinal = JsonConvert.DeserializeObject<Domain.Modules.Country.Country>(respRedis);
                return await Task.FromResult(respFinal);
            }
        }
    }
}

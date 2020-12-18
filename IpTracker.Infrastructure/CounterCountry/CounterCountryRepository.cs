using IpTracker.Domain.Modules.CounterCountry;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Infrastructure.CounterCountry
{
    public class CounterCountryRepository : ICounterCountryRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private const string REPOKEY = "CountersCountries";

        public CounterCountryRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }
        public async Task<List<Domain.Modules.CounterCountry.CounterCountry>> GetAsync()
        {
            var redisDb = _connectionMultiplexer.GetDatabase();
            var respRedis = await redisDb.StringGetAsync(REPOKEY);
            if (respRedis.IsNull)
                return new List<Domain.Modules.CounterCountry.CounterCountry>();
            else
            {
                var respFinal = JsonConvert.DeserializeObject<List<Domain.Modules.CounterCountry.CounterCountry>>(respRedis);
                return await Task.FromResult(respFinal);
            }
        }

        public void Increment(Domain.Modules.CounterCountry.CounterCountry counter)
        {
            List<Domain.Modules.CounterCountry.CounterCountry> list = new List<Domain.Modules.CounterCountry.CounterCountry>();
            var redisDb = _connectionMultiplexer.GetDatabase();
            var respRedis = redisDb.StringGet(REPOKEY);
            if (respRedis.IsNull)
            {
                counter.Count++;
                list.Add(counter);
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<Domain.Modules.CounterCountry.CounterCountry>>(respRedis);
                if (list.Exists(c => c.CountryCode == counter.CountryCode))
                    list.FirstOrDefault(c => c.CountryCode == counter.CountryCode).Count++;
                else 
                {
                    counter.Count++;
                    list.Add(counter);
                }
            }
            _ = redisDb.StringSetAsync(REPOKEY, JsonConvert.SerializeObject(list));
        }
    }
}

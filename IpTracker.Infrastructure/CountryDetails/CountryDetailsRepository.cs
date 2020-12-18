using IpTracker.Domain.Modules.CountryDetails;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Infrastructure.CountryDetails
{
    public class CountryDetailsRepository : ICountryDetailsRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private const string REPOKEY = "CountryDetails-";

        public CountryDetailsRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public void Add(Domain.Modules.CountryDetails.CountryDetails countryDetails)
        {
            var redisDb = _connectionMultiplexer.GetDatabase();
            redisDb.StringSetAsync(REPOKEY + countryDetails.CountryCode, JsonConvert.SerializeObject(countryDetails));
        }

        public async Task<Domain.Modules.CountryDetails.CountryDetails> GetAsync(string countryCode)
        {
            var redisDb = _connectionMultiplexer.GetDatabase();
            var respRedis = await redisDb.StringGetAsync(REPOKEY + countryCode);
            if (respRedis.IsNull)
                return null;
            else
            {
                var respFinal = JsonConvert.DeserializeObject<Domain.Modules.CountryDetails.CountryDetails>(respRedis);
                return await Task.FromResult(respFinal);
            }
        }
    }
}

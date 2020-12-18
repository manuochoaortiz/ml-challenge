using Flurl.Http;
using IpTracker.Domain.Modules.Country;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Infrastructure.Country
{
    public class CountryService : ICountryService
    {
        private readonly string _serviceUrl = "";

        public CountryService(string serviceUrl)
        {
            _serviceUrl = serviceUrl;
        }
        public async Task<Domain.Modules.Country.Country> GetAsync(string ip)
        {
            string urlHttp = string.Format(_serviceUrl, ip);
            var taskGet = await urlHttp.GetJsonAsync<Ip2Country>();
            return await Map(ip, taskGet);
        }

        private async Task<Domain.Modules.Country.Country> Map(string ip, Ip2Country country)
        {
            return await Task.FromResult(new Domain.Modules.Country.Country()
            {
                Ip = ip,
                CountryCode = country.countryCode,
                CountryCode3 = country.countryCode3,
                CountryName = country.countryName,
            });
        }
    }
}

using Flurl.Http;
using IpTracker.Domain.Modules.CountryDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Infrastructure.CountryDetails
{
    public class CountryDetailsService : ICountryDetailsService
    {
        private readonly string _serviceUrl = "";
        public CountryDetailsService(string serviceUrl)
        {
            _serviceUrl = serviceUrl;
        }

        public async Task<Domain.Modules.CountryDetails.CountryDetails> GetAsync(string countryCode)
        {
            string urlHttp = string.Format(_serviceUrl, countryCode);
            var taskGet = await urlHttp.GetJsonAsync<RestCountry>();
            return await Map(countryCode, taskGet);
        }

        private async Task<Domain.Modules.CountryDetails.CountryDetails> Map(string countryCode, RestCountry countryDetails)
        {
            return await Task.FromResult(new Domain.Modules.CountryDetails.CountryDetails()
            {
                CountryCode = countryCode,
                Languages = countryDetails.Languages != null
                            ? countryDetails.Languages.Select(p => p.name).ToList() : new List<string>(),
                CurrencyCodes = countryDetails.Currencies != null
                            ? countryDetails.Currencies.Select(p => p.code).ToList() : new List<string>(),
                Timezones = countryDetails.Timezones != null
                            ? countryDetails.Timezones.Select(p => p).ToList() : new List<string>(),
                Lat = countryDetails.Latlng != null && countryDetails.Latlng.Count == 2
                            ? float.Parse(countryDetails.Latlng[0].Replace(".", ",")) : 0,
                Lon = countryDetails.Latlng != null && countryDetails.Latlng.Count == 2
                            ? float.Parse(countryDetails.Latlng[1].Replace(".", ",")) : 0
            });
        }
    }
}

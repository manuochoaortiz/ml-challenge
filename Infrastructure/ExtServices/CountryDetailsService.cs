using Domain.Entities;
using Domain.Infrastructure.ExtServices;
using Flurl.Http;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExtServices
{
    public class CountryDetailsService : IServices<CountryDetails>
    {
        private string urlService = "https://restcountries.eu/rest/v2/alpha/{0}";

        public async Task<CountryDetails> GetEntityByKey(string key)
        {
            try
            {
                string urlHttp = string.Format(urlService, key);
                var taskGet = await urlHttp.GetJsonAsync<RestCountry>();

                CountryDetails countryDetails = new CountryDetails()
                {
                    Languages = taskGet.Languages != null 
                                ? taskGet.Languages.Select(p => p.name).ToList() : new List<string>(),
                    Currencies = taskGet.Currencies != null
                                ? taskGet.Currencies.Select(p => p.name).ToList() : new List<string>(),
                    Timezones = taskGet.Timezones != null
                                ? taskGet.Timezones.Select(p => p).ToList() : new List<string>(),
                };

                return countryDetails;
            }
            catch (Exception)
            {

                CountryDetails countryDetails = new CountryDetails()
                {
                    Languages =  new List<string>(),
                    Currencies = new List<string>(),
                    Timezones = new List<string>(),
                };
                return countryDetails;
            }
        }
    }
}

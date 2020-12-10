using Domain.Entities;
using Domain.Infrastructure.ExtServices;
using Flurl.Http;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExtServices
{
    public class IpCountryService : IServices<IpCountry>
    {
        private string urlService = "https://api.ip2country.info/ip?{0}";

        public async Task<IpCountry> GetEntityByKey(string key)
        {
            try
            {
                string urlHttp = string.Format(urlService, key);
                var taskGet = await urlHttp.GetJsonAsync<Ip2Country>();

                IpCountry ipCountry = new IpCountry()
                {
                    countryCode = taskGet.countryCode,
                    countryCode3 = taskGet.countryCode3,
                    countryName = taskGet.countryName,
                };

                return ipCountry;
            }
            catch (Exception)
            {

                IpCountry ipCountry = new IpCountry()
                {
                    countryCode = "",
                    countryCode3 = "",
                    countryName = ""
                };
                return ipCountry;
            }
        }
    }
}

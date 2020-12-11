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
    public class CurrencyService : IServices<Currency>
    {
        private string urlService = "https://free.currconv.com/api/v7/convert?q=USD_COP&compact=ultra&apiKey=b843ba8979189b60a3f3";

        public async Task<Currency> GetEntityByKey(string key)
        {
            Currency entity = new Currency();
            try
            {
                string urlHttp = string.Format(urlService, key);
                var resoList = await urlHttp.GetJsonAsync<Dictionary<string, string>>();

                foreach (var pair in resoList)
                {
                    entity = new Currency()
                    {
                        Pair = pair.Key,
                        Value = decimal.Parse(pair.Value)
                    };
                }
            }
            catch (Exception ex)
            {
            }

            return entity;
        }
    
    }
}

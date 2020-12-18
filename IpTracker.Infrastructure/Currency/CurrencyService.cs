using Flurl.Http;
using IpTracker.Domain.Modules.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Infrastructure.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly string _serviceUrl = "";
        public CurrencyService(string serviceUrl)
        {
            _serviceUrl = serviceUrl;
        }

        public async Task<Domain.Modules.Currency.Currency> GetAsync(string currencyCode)
        {
            string pair = string.Format("{0}_{1}", GetCurrencyCodeExchange(currencyCode), currencyCode);
            string urlHttp = string.Format(_serviceUrl, pair);
            var taskGet = await urlHttp.GetJsonAsync<Dictionary<string, string>>();
            return await Map(currencyCode, taskGet);
        }

        private async Task<Domain.Modules.Currency.Currency> Map(string currencyCode, Dictionary<string, string> currencyResponse)
        {
            Domain.Modules.Currency.Currency entity = new Domain.Modules.Currency.Currency();
            foreach (var pair in currencyResponse)
            {
                entity = new Domain.Modules.Currency.Currency()
                {
                    CurrencyCode = currencyCode,
                    CurrencyCodeExchange = GetCurrencyCodeExchange(currencyCode),
                    Value = decimal.Parse(pair.Value.Replace(".", ","))
                };
                break;
            }
            return await Task.FromResult(entity);
        }
        private static string GetCurrencyCodeExchange(string currencyCode)
        {
            return currencyCode == "USD" ? "EUR" : "USD";
        }
    }
}

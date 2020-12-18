using IpTracker.Domain.Modules.Country;
using IpTracker.Domain.Modules.CountryDetails;
using IpTracker.Domain.Modules.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IpTracker.Api.Application.Queries
{
    public class GetIpTrackByIpViewModel
    {
        public string IP { get; private set; }
        [JsonPropertyName("País")]
        public string CountryName { get; private set; }
        [JsonPropertyName("ISO Code")]
        public string CountryIso { get; private set; }
        [JsonPropertyName("Idiomas")]
        public IList<string> Languajes { get; private set; }
        [JsonPropertyName("Fecha Actual")]
        public IList<string> CurrentDateTimes { get; private set; }
        [JsonPropertyName("Distancia estimada")]
        public string DistanceTo { get; private set; }
        [JsonPropertyName("Moneda")]
        public string Currency { get; private set; }
        [JsonIgnore]
        public decimal Distance { get; private set; }

        public void Load(Country country, CountryDetails countryDetails, Currency currency)
        {
            const double LatBA = -34;
            const double LonBA = -58;

            IP = country.Ip;
            CountryIso = country.CountryCode;
            CountryName = country.CountryName;
            Languajes = countryDetails.Languages ?? new List<string>();
            CurrentDateTimes = countryDetails.CalcCurrentDateTimes();
            Currency = string.Format("{0} (1 {1} = {2} {0})", currency.CurrencyCode
                                                            , currency.CurrencyCodeExchange
                                                            , currency.Value);
            Distance = countryDetails.CalculateDistanceFrom(LatBA, LonBA);
            DistanceTo = string.Format("{0} KM de ({1}, {2}) a ({3}, {4})", Distance
                                                                            , LatBA
                                                                            , LonBA
                                                                            , countryDetails.Lat
                                                                            , countryDetails.Lon);

        }
    }
}

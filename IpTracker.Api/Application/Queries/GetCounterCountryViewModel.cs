using IpTracker.Domain.Modules.CounterCountry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IpTracker.Api.Application.Queries
{
    public class GetCounterCountryViewModel
    {
        [JsonPropertyName("Distancia mas lejana")]
        public string FarDistance { get; set; }
        [JsonPropertyName("Distancia mas cercana")]
        public string NearDistance { get; set; }
        [JsonPropertyName("Distancia promedio")]
        public string AverageDistance { get; set; }
        [JsonPropertyName("Estadisticas")]
        public IList<CounterItemViewModel> CounterItems { get; private set; }

        public void Load(CounterCountryReport domainCounter)
        {
            if (domainCounter.List.Count > 0)
            { 
                CounterItems = domainCounter.List
                    .Select(c => CounterItemViewModel.GeCounterItemViewModel(c))
                    .OrderByDescending(c => c.Count)
                    .ToList();

                FarDistance = FormatPlaceDistance(domainCounter.GetFarCountry());
                NearDistance = FormatPlaceDistance(domainCounter.GetNearCountry());
                AverageDistance = string.Format("{0} KM", domainCounter.GetAverageDistance());
            }
        }

        private static string FormatPlaceDistance(CounterCountry counterCountry)
        {
            return string.Format("{0} {1} KM", counterCountry.CountryName, Math.Truncate(counterCountry.Distance));
        }
    }

    public class CounterItemViewModel
    {
        [JsonPropertyName("Pais")]
        public string CountryName { get; set; }
        [JsonPropertyName("Distancia")]
        public string Distance { get; set; }
        [JsonPropertyName("Invocaciones")]
        public int Count { get; set; }

        public static CounterItemViewModel GeCounterItemViewModel(CounterCountry counterCountry)
        {
            return new CounterItemViewModel()
            {
                CountryName = counterCountry.CountryName,
                Distance = string.Format("{0} KM", Math.Truncate(counterCountry.Distance)),
                Count = counterCountry.Count
            };
        }
    }
}

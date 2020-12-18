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

        public void Load(List<CounterCountry> listCounterCountry)
        {
            if (listCounterCountry.Count > 0)
            { 
                CounterItems = listCounterCountry
                    .Select(c => GetCounterCountryItem(c))
                    .OrderByDescending(c => c.Count)
                    .ToList();

                var farCountry = listCounterCountry.OrderByDescending(c => c.Distance).FirstOrDefault();
                FarDistance = FormatPlaceDistance(farCountry);

                var nearCountry = listCounterCountry.OrderBy(c => c.Distance).FirstOrDefault();
                NearDistance = FormatPlaceDistance(nearCountry);

                AverageDistance = string.Format("{0} KM", GetAverageDistance(listCounterCountry));
            }
        }

        private static string FormatPlaceDistance(CounterCountry counterCountry)
        {
            return string.Format("{0} {1} KM", counterCountry.CountryName, Math.Truncate(counterCountry.Distance));
        }

        private static decimal GetAverageDistance(List<CounterCountry> listCounterCountry)
        {
            decimal sum = 0;
            for (int i = 0; i < listCounterCountry.Count; i++)
                sum += listCounterCountry[i].Distance * listCounterCountry[i].Count;
            decimal average = sum / listCounterCountry.Sum(c => c.Count);
            return Math.Truncate(average);
        }

        private static CounterItemViewModel GetCounterCountryItem(CounterCountry counterCountry)
        {
            return new CounterItemViewModel()
            {
                CountryName = counterCountry.CountryName,
                Distance = string.Format("{0} KM", Math.Truncate(counterCountry.Distance)),
                Count = counterCountry.Count
            };
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
    }
}

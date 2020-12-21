using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTracker.Domain.Modules.CounterCountry
{
    public class CounterCountryReport : IAggregateRoot
    {
        public List<CounterCountry> List { get; set; }

        public decimal GetAverageDistance()
        {
            decimal sum = 0;
            for (int i = 0; i < List.Count; i++)
                sum += List[i].Distance * List[i].Count;
            decimal average = sum / List.Sum(c => c.Count);
            return Math.Truncate(average);
        }

        public CounterCountry GetFarCountry()
        {
            var farCountry = List.OrderByDescending(c => c.Distance).FirstOrDefault();
            return farCountry;
        }
        public CounterCountry GetNearCountry()
        {
            var nearCountry = List.OrderBy(c => c.Distance).FirstOrDefault();
            return nearCountry;
        }
    }
}

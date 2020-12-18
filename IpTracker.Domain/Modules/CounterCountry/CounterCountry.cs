using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpTracker.Domain.Modules.CounterCountry
{
    public class CounterCountry : IAggregateRoot
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public decimal Distance { get; set; }
        public int Count { get; set; }
    }
}

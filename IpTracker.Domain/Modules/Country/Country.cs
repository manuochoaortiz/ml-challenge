using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpTracker.Domain.Modules.Country
{
    public class Country : IAggregateRoot
    {
        public string Ip { get; set; }
        public string CountryCode { get; set; }
        public string CountryCode3 { get; set; }
        public string CountryName { get; set; }
    }
}

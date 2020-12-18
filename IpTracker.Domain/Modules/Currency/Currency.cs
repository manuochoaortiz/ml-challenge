using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpTracker.Domain.Modules.Currency
{
    public class Currency : IAggregateRoot
    {
        public string CurrencyCode { get; set; }
        public string CurrencyCodeExchange { get; set; }
        public decimal Value { get; set; }
    }
}

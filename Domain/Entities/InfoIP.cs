using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class InfoIP
    {
        public string IP { get; set; }
        public Country Country { get; set; }
        public Currency Currency { get; set; }
        public Geo Geo { get; set; }

        public InfoIP(string ip)
        {
            IP = ip;
        }

        public InfoIP WithCountry(Country country)
        {
            Country = country;
            return this;
        }

        public InfoIP WithCurrency(Currency currency)
        {
            Currency = currency;
            return this;
        }

        public InfoIP WithGeo(Geo geo)
        {
            Geo = geo;
            return this;
        }
    }
}

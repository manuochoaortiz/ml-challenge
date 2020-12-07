using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class InfoIP
    {
        //public InfoIP()
        //{
        //    _ip2Country = new Ip2Country();
        //    _restCountry = new RestCountry();
        //}


        public string IP { get; set; }
        private Ip2Country _ip2Country { get; set; }
        public string CountryName { get { return _ip2Country.countryName; } }
        public string CountryIso { get { return _ip2Country.countryCode; } }

        private RestCountry _restCountry { get; set; }
        public string Languajes { get {
                return _restCountry != null && _restCountry.Languages != null
                    ? string.Join(",", _restCountry.Languages.Select(p => p.name)) : "";  
            }}

        public string Currencies { get {
                return _restCountry != null && _restCountry.Currencies != null
                    ? string.Join(",", _restCountry.Currencies.Select(p => p.name)) : "";
            }
        }

        private Currency Currency { get; set; }
        private Geo Geo { get; set; }

        public InfoIP(string ip)
        {
            IP = ip;
        }

        public InfoIP WithIp2Country(Ip2Country country)
        {
            _ip2Country = country;
            return this;
        }

        public InfoIP WithRestCountry(RestCountry restCountry)
        {
            _restCountry = restCountry;
            return this;
        }

        public InfoIP WithGeo(Geo geo)
        {
            Geo = geo;
            return this;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class RestCountry
    {
        public IList<RestCountryLanguage> Languages { get; set; }
        public IList<RestCountryCurrency> Currencies { get; set; }
        public IList<string> Timezones { get; set; }
    }

    public class RestCountryLanguage
    {
        public string iso639_1 { get; set; }
        public string iso639_2 { get; set; }
        public string name { get; set; }
        public string nativeName { get; set; }
    }

    public class RestCountryCurrency
    {
        public string code { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
    }
}

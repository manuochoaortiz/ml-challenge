using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CountryDetails
    {
        public IList<string> Languages { get; set; }
        public IList<string> Currencies { get; set; }
        public IList<string> Timezones { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
//using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class InfoIP
    {
        public string IP { get; set; }
        public string CountryName { get; private set; }
        public string CountryIso { get; private set; }
        public IList<string> Languajes { get; private set; }
        public IList<string> Currencies { get; private set; }
        public IList<string> CurrentTimes { get; private set; }

        [JsonIgnore]
        public IList<string> Timezones { get; private set; }

        public InfoIP(string ip, IpCountry country, CountryDetails countryDetails)
        {
            IP = ip;

            this.CountryName = country.countryName;
            this.CountryIso = country.countryCode;

            this.Languajes = countryDetails.Languages ?? new List<string>();
            this.Currencies = countryDetails.Currencies ?? new List<string>();
            this.Timezones = countryDetails.Timezones ?? new List<string>();
        }

        public void CalculateCurrentDate()
        {
            DateTime utcNow = DateTime.UtcNow;
            this.CurrentTimes = this.Timezones.Select(p => CurrentDate(p, utcNow)).ToList() ?? new List<string>();
        }

        private string CurrentDate(string timeZone, DateTime utcNow)
        {
            int hhTimezone = 0;
            int mmTimezone = 0;

            var arrayTimeZone = timeZone.Split(":");
            if (arrayTimeZone != null && arrayTimeZone.Length > 0)
            { 
                string _hhTimezone = arrayTimeZone[0].Substring(arrayTimeZone[0].Length - 3, 2);
                hhTimezone = int.Parse(_hhTimezone);
                string _mmTimezone = arrayTimeZone[1].Substring(arrayTimeZone[1].Length - 2, 2);
                mmTimezone = int.Parse(_mmTimezone);
            }

            TimeSpan offset = new TimeSpan(hhTimezone, mmTimezone, 00);
            TimeZoneInfo mawson = TimeZoneInfo.CreateCustomTimeZone(timeZone, offset, timeZone, timeZone);
            var newDate = TimeZoneInfo.ConvertTimeFromUtc(utcNow, mawson);

            return timeZone + " - " + newDate.ToString("HH:mm dd/MM/yyyy");
        }

    }
}

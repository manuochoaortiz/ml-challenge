using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTracker.Domain.Modules.CountryDetails
{
    public class CountryDetails : IAggregateRoot
    {
        public string CountryCode { get; set; }
        public IList<string> Languages { get; set; }
        public IList<string> CurrencyCodes { get; set; }
        public IList<string> Timezones { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public decimal CalculateDistanceFrom(double latFrom, double lonFrom)
        {
            var RadianLatA = Math.PI * Lat / 180;
            var RadianLatb = Math.PI * latFrom / 180;
            var RadianLongA = Math.PI * Lon / 180;
            var RadianLongB = Math.PI * lonFrom / 180;

            double theDistance = (Math.Sin(RadianLatA)) *
                    Math.Sin(RadianLatb) +
                    Math.Cos(RadianLatA) *
                    Math.Cos(RadianLatb) *
                    Math.Cos(RadianLongA - RadianLongB);

            return Math.Truncate(Convert.ToDecimal(((Math.Acos(theDistance) * (180.0 / Math.PI)))) * 69.09M * 1.6093M);
        }

        public IList<string> CalcCurrentDateTimes()
        {
            DateTime utcNow = DateTime.UtcNow;
            IList<string> CurrentDateTimes = Timezones.Select(p => CurrentDateByTimeZone(p, utcNow)).ToList() ?? new List<string>();
            return CurrentDateTimes;
        }

        private static string CurrentDateByTimeZone(string timeZone, DateTime utcNow)
        {
            int hhTimezone = 0;
            int mmTimezone = 0;

            var arrayTimeZone = timeZone.Split(":");
            if (arrayTimeZone != null && arrayTimeZone.Length == 2)
            {
                string _hhTimezone = arrayTimeZone[0].Substring(arrayTimeZone[0].Length - 3, 3);
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

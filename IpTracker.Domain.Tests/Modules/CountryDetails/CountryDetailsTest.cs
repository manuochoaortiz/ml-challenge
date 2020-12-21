using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace IpTracker.Domain.Tests.Modules.CountryDetails
{
    [ExcludeFromCodeCoverage]
    public class CountryDetailsTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ShouldHasCurrency()
        {
            var countryDetails = new Domain.Modules.CountryDetails.CountryDetails()
            {
                CountryCode = "CO",
                Languages = new List<string>(),
                CurrencyCodes = new List<string>() { "COP" },
                Timezones = new List<string>(),
                Lat = 0,
                Lon = 0
            };

            Assert.IsTrue(countryDetails.HasCurrency());
        }

        [Test]
        public void ShouldNotHasCurrency()
        {
            var countryDetails = new Domain.Modules.CountryDetails.CountryDetails()
            {
                CountryCode = "CO",
                Languages =  new List<string>(),
                CurrencyCodes =  new List<string>(),
                Timezones = new List<string>(),
                Lat =  0,
                Lon =  0
            };
            
            Assert.IsFalse(countryDetails.HasCurrency());
        }

        [Test]
        public void ShouldCalculateDistanceFrom()
        {
            var countryDetails = new Domain.Modules.CountryDetails.CountryDetails()
            {
                CountryCode = "CO",
                Languages = new List<string>(),
                CurrencyCodes = new List<string>(),
                Timezones = new List<string>(),
                Lat = 77,
                Lon = 34
            };
            var distance = countryDetails.CalculateDistanceFrom(77, 34);
            Assert.AreEqual(0, distance);
        }

        [Test]
        public void ShouldCalculateDateTimes()
        {
            var countryDetails = new Domain.Modules.CountryDetails.CountryDetails()
            {
                CountryCode = "CO",
                Languages = new List<string>(),
                CurrencyCodes = new List<string>(),
                Timezones = new List<string>() { "UTC+01:00", "UTC-01:30", "UTC" },
                Lat = 77,
                Lon = 34
            };
            DateTime utcNow = DateTime.UtcNow;
            DateTime dateTimeZone1 = utcNow.AddHours(1);
            var datetimesClaculated = countryDetails.CalcCurrentDateTimes();

            Assert.AreEqual("UTC+01:00" + " - " + dateTimeZone1.ToString("HH:mm dd/MM/yyyy"), datetimesClaculated[0]);
            Assert.AreEqual("UTC" + " - " + utcNow.ToString("HH:mm dd/MM/yyyy"), datetimesClaculated[2]);
        }
    }
}

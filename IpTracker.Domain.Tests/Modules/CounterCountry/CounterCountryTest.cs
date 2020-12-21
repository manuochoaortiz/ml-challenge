using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpTracker.Domain.Tests.Modules.CounterCountry
{
    public class CounterCountryTest
    {
        Domain.Modules.CounterCountry.CounterCountry _countryBrazil;
        Domain.Modules.CounterCountry.CounterCountry _countrySpain;
        Domain.Modules.CounterCountry.CounterCountryReport _counterCountryReport;
        [SetUp]
        public void Setup()
        {
            _countryBrazil = new Domain.Modules.CounterCountry.CounterCountry()
            {
                CountryCode = "BR",
                CountryName = "Brazil",
                Count = 10,
                Distance = 2862
            };
            _countrySpain = new Domain.Modules.CounterCountry.CounterCountry()
            {
                CountryCode = "ES",
                CountryName = "Spain",
                Count = 5,
                Distance = 10040
            };
            _counterCountryReport = new Domain.Modules.CounterCountry.CounterCountryReport()
            {
                List = new List<Domain.Modules.CounterCountry.CounterCountry>()
                {
                    _countryBrazil, _countrySpain
                }
            };
        }

        [Test]
        public void ShoulGetFarCountry()
        {
            var respFarCountry = _counterCountryReport.GetFarCountry();
            Assert.AreEqual(_countrySpain, respFarCountry);
        }

        [Test]
        public void ShoulGetNearCountry()
        {
            var respNearCountry = _counterCountryReport.GetNearCountry();
            Assert.AreEqual(_countryBrazil, respNearCountry);
        }

        [Test]
        public void ShoulGetAverageDistance()
        {
            var respAverage = _counterCountryReport.GetAverageDistance();
            Assert.AreEqual(5254, respAverage);
        }
    }
}

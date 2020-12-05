using Domain.Entities;
using Domain.Infrastructure.ExtServices;
using Domain.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Application
{
    public class TracerApplication : ITracerApplication
    {
        private readonly ICountriesService _countriesService;
        private readonly ICurrencyService _currencyService;
        private readonly IGeoService _geoService;

        private readonly ICountriesRepo _countriesRepo;
        private readonly ICurrencyRepo _currencyRepo;
        private readonly IGeoRepo _geoRepo;

        public TracerApplication(ICountriesService countriesService,
                                ICurrencyService currencyService,
                                IGeoService geoService,
                                ICountriesRepo countriesRepo,
                                ICurrencyRepo currencyRepo,
                                IGeoRepo geoRepo)
        {
            _countriesService = countriesService;
            _currencyService = currencyService;
            _geoService = geoService;
            _countriesRepo = countriesRepo;
            _currencyRepo = currencyRepo;
            _geoRepo = geoRepo;
        }

        public InfoIP ByIP(string ip)
        {
            throw new NotImplementedException();
        }
    }
}

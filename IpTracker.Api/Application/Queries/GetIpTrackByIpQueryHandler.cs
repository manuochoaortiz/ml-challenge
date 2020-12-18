using IpTracker.Domain.Modules.CounterCountry;
using IpTracker.Domain.Modules.Country;
using IpTracker.Domain.Modules.CountryDetails;
using IpTracker.Domain.Modules.Currency;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IpTracker.Api.Application.Queries
{
    public class GetIpTrackByIpQueryHandler : IRequestHandler<GetIpTrackByIpQuery, GetIpTrackByIpViewModel>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICountryService _countryService;
        private readonly ICountryDetailsRepository _countryDetailsRepository;
        private readonly ICountryDetailsService _countryDetailsService;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICurrencyService _currencyService;
        private readonly ICounterCountryRepository _counterCountryRepository;
        
        public GetIpTrackByIpQueryHandler(ICountryRepository countryRepository, 
                                        ICountryService countryService,
                                        ICountryDetailsRepository countryDetailsRepository,
                                        ICountryDetailsService countryDetailsService,
                                        ICurrencyRepository currencyRepository,
                                        ICurrencyService currencyService,
                                        ICounterCountryRepository counterCountryRepository)
        {
            _countryRepository = countryRepository;
            _countryService = countryService;
            _countryDetailsRepository = countryDetailsRepository;
            _countryDetailsService = countryDetailsService;
            _currencyRepository = currencyRepository;
            _currencyService = currencyService;
            _counterCountryRepository = counterCountryRepository;

        }

        public async Task<GetIpTrackByIpViewModel> Handle(GetIpTrackByIpQuery request, CancellationToken cancellationToken)
        {
            var viewModel = new GetIpTrackByIpViewModel();

            //Query Country By IP
            var countryTask = _countryRepository.GetAsync(request.Ip);
            if (countryTask.Result == null)
            {
                countryTask = _countryService.GetAsync(request.Ip);
                if (countryTask.Result == null)
                    throw new ApplicationException("Ip not found");
                _ = Task.Run(() => _countryRepository.Add(countryTask.Result));
            }

            //Query Country Details By CountryCode
            string countryCode = countryTask.Result.CountryCode;
            var countryDetailsTask = _countryDetailsRepository.GetAsync(countryCode);
            if (countryDetailsTask.Result == null)
            {
                countryDetailsTask = _countryDetailsService.GetAsync(countryCode);
                if (countryDetailsTask.Result == null)
                    throw new ApplicationException("Country not found");
                _ = Task.Run(() => _countryDetailsRepository.Add(countryDetailsTask.Result));
            }

            //Query Currency By Country CurrencyCode
            Currency currency = null;
            if (countryDetailsTask.Result.HasCurrency())
            { 
                string currencyCode = countryDetailsTask.Result.CurrencyCodes.FirstOrDefault();
                var currencyTask = _currencyRepository.GetAsync(currencyCode);
                if (currencyTask.Result == null)
                {
                    currencyTask = _currencyService.GetAsync(currencyCode);
                    if (currencyTask.Result != null)
                        _ = Task.Run(() => _currencyRepository.Add(currencyTask.Result));
                }
                currency = currencyTask.Result;
            }

            //Load info in viewModel
            viewModel.Load(countryTask.Result, countryDetailsTask.Result, currency);

            //Log Counter for Country in new thread
            _ = Task.Run(() =>
                _counterCountryRepository.Increment(new CounterCountry()
                {
                    CountryCode = viewModel.CountryIso,
                    CountryName = viewModel.CountryName,
                    Distance = viewModel.Distance
                })
            );

            return await Task.FromResult(viewModel);
        }
    }
}

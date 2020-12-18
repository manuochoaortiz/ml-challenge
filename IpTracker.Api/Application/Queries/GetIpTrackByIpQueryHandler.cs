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

            var countryTask = _countryRepository.GetAsync(request.Ip);
            if (countryTask.Result == null)
            {
                countryTask = _countryService.GetAsync(request.Ip);
                if (countryTask.Result == null)
                    throw new ApplicationException("Ip not found");
                _ = Task.Run(() => _countryRepository.Add(countryTask.Result));
            }

            string countryCode = countryTask.Result.CountryCode;
            var countryDetailsTask = _countryDetailsRepository.GetAsync(countryCode);
            if (countryDetailsTask.Result == null)
            {
                countryDetailsTask = _countryDetailsService.GetAsync(countryCode);
                _ = Task.Run(() => _countryDetailsRepository.Add(countryDetailsTask.Result));
            }

            string currencyCode = countryDetailsTask.Result.CurrencyCodes.FirstOrDefault();
            var currencyTask = _currencyRepository.GetAsync(currencyCode);
            if (currencyTask.Result == null)
            {
                currencyTask = _currencyService.GetAsync(currencyCode);
                _ = Task.Run(() => _currencyRepository.Add(currencyTask.Result));
            }

            viewModel.Load(countryTask.Result, countryDetailsTask.Result, currencyTask.Result);

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

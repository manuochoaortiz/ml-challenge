using IpTracker.Domain.Modules.CounterCountry;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IpTracker.Api.Application.Queries
{
    public class GetCounterCountryQueryHandler : IRequestHandler<GetCounterCountryQuery, GetCounterCountryViewModel>
    {
        private readonly ICounterCountryRepository _counterCountryRepository;
        public GetCounterCountryQueryHandler(ICounterCountryRepository counterCountryRepository)
        {
            _counterCountryRepository = counterCountryRepository;
        }
        public async Task<GetCounterCountryViewModel> Handle(GetCounterCountryQuery request, CancellationToken cancellationToken)
        {
            var viewModel = new GetCounterCountryViewModel();
            var listCounterCountry = await _counterCountryRepository.GetAsync();

            viewModel.Load(new CounterCountryReport() { List = listCounterCountry } );
            return await Task.FromResult(viewModel);
        }
    }
}

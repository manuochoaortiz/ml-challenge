using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Domain.Modules.CountryDetails
{
    public interface ICountryDetailsRepository : IRepository<CountryDetails>
    {
        void Add(CountryDetails countryDetails);
        Task<CountryDetails> GetAsync(string countryCode);
    }
}

using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Domain.Modules.CountryDetails
{
    public interface ICountryDetailsService : IService<CountryDetails>
    {
        Task<CountryDetails> GetAsync(string countryCode);
    }
}

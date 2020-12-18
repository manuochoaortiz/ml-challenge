using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Domain.Modules.Country
{
    public interface ICountryRepository : IRepository<Country>
    {
        void Add(Country country);
        Task<Country> GetAsync(string ip);
    }
}

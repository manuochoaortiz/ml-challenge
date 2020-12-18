using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Domain.Modules.Country
{
    public interface ICountryService : IService<Country>
    {
        Task<Country> GetAsync(string ip);
    }
}

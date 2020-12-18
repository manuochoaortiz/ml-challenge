using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Domain.Modules.CounterCountry
{
    public interface ICounterCountryRepository : IRepository<CounterCountry>
    {
        void Increment(CounterCountry counter);
        Task<List<CounterCountry>> GetAsync();
    }
}

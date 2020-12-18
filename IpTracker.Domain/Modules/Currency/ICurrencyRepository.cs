using IpTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpTracker.Domain.Modules.Currency
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        void Add(Currency currency);
        Task<Currency> GetAsync(string currencyCode);
    }
}

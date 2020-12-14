using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Abstractions
{
    public interface ICurrencyApplication
    {
        Task<T> GetCurrencyFromRepo<T>(string code);
        Task<Currency> GetCurrencyFromService(string code);
        void SaveCurrencyToRepo(string code, Currency entity);
    }
}

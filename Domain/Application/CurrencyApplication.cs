using Domain.Application.Abstractions;
using Domain.Entities;
using Domain.Infrastructure;
using Domain.Infrastructure.ExtServices;
using Domain.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Application
{
    public class CurrencyApplication : BaseApplication, ICurrencyApplication
    {
        //private readonly IRepo _repo;
        private readonly IServices<Currency> _currencyService;

        public CurrencyApplication(IRepo repo,
                                IServices<Currency> currencyService) : base(repo)
        {
            //_repo = repo;
            _currencyService = currencyService;
        }

        public async Task<T> GetCurrencyFromRepo<T>(string ip)
        {
            string EntityRepoKey = "IpCountry";
            var entityTask = base.GetEntityFromRepo<T>(EntityRepoKey + ip);
            return await entityTask;
        }
        public void SaveCurrencyToRepo(string ip, Currency entity)
        {
            string EntityRepoKey = "IpCountry";
            base.SaveRepoEntity<Currency>(EntityRepoKey + ip, entity);
        }
        public async Task<Currency> GetCurrencyFromService(string ip)
        {
            var entityTask = _currencyService.GetEntityByKey(ip);
            return await entityTask;
        }

    }
}

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
    public class CountryApplication : BaseApplication, ICountryApplication
    {
        private readonly IServices<IpCountry> _ipCountryService;
        private readonly IServices<CountryDetails> _countryDetailsService;

        public CountryApplication(IRepo repo,
                                IServices<IpCountry> ipCountryService,
                                IServices<CountryDetails> countryDetailsService) : base(repo)
        {
            _ipCountryService = ipCountryService;
            _countryDetailsService = countryDetailsService;
        }

        public async Task<T> GetIpCountryFromRepo<T>(string ip)
        {
            string EntityRepoKey = "IpCountry";
            var entityTask = base.GetEntityFromRepo<T>(EntityRepoKey + ip);
            return await entityTask;
        }
        public void SaveIpCountryToRepo(string ip, IpCountry entity)
        {
            string EntityRepoKey = "IpCountry";
            base.SaveRepoEntity<IpCountry>(EntityRepoKey + ip, entity);
        }
        public async Task<IpCountry> GetIpCountryFromService(string ip)
        {
            var entityTask = _ipCountryService.GetEntityByKey(ip);
            return await entityTask;
        }

        public async Task<T> GetCountryDetailsFromRepo<T>(string cod)
        {
            string EntityRepoKey = "CountryDetails";
            var entityTask = base.GetEntityFromRepo<T>(EntityRepoKey + cod);
            return await entityTask;
        }
        public void SaveCountryDetailsToRepo(string cod, CountryDetails entity)
        {
            string EntityRepoKey = "CountryDetails";
            base.SaveRepoEntity<CountryDetails>(EntityRepoKey + cod, entity);
        }

        public async Task<CountryDetails> GetCountryDetailsFromService(string cod)
        {
            var entityTask = _countryDetailsService.GetEntityByKey(cod);
            return await entityTask;
        }


        //Common
        //private async Task<T> GetEntityFromRepo<T>(string key)
        //{
        //    var entityTask = _repo.GetByKey<T>(key);
        //    return await entityTask;
        //}

        //private async void SaveRepoEntity<T>(string key, T result)
        //{
        //    await Task.Run(() =>
        //        _repo.SetForKey<T>(key, result)
        //    );
        //}
    }
}

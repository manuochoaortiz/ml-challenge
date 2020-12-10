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
    public class TracerApplication : ITracerApplication
    {
        private readonly IRepo _repo;
        private readonly IServices<IpCountry> _ipCountryService;
        private readonly IServices<CountryDetails> _countryDetailsService;
        
        public TracerApplication(IRepo repo,
                                IServices<IpCountry> ipCountryService,
                                IServices<CountryDetails> countryDetailsService)
        {
            _repo = repo;
            _ipCountryService = ipCountryService;
            _countryDetailsService = countryDetailsService;
        }

        public async Task<InfoIP> ByIP(string ip)
        {
            var ip2CountryTask = this.GetIpCountryFromRepoOrService(ip);
            var restCountryTask = this.GetCountryDetailsFromRepoOrService(ip2CountryTask.Result.countryCode);

            InfoIP info = new InfoIP(ip, ip2CountryTask.Result, restCountryTask.Result);

            info.CalculateCurrentDate();
            return await Task.FromResult(info);
        }

        private async Task<IpCountry> GetIpCountryFromRepoOrService(string ip)
        {
            var objectTask = _repo.GetByKey<IpCountry>("IpCountry" + ip);
            if (objectTask.Result == null)
            {
                objectTask = _ipCountryService.GetEntityByKey(ip);

                Thread repoSetThread = new Thread(() =>
                    _repo.SetForKey<IpCountry>("IpCountry" + ip, objectTask.Result)
                );
                repoSetThread.Start();
            }

            return await objectTask;
        }

        private async Task<CountryDetails> GetCountryDetailsFromRepoOrService(string cod)
        {
            var objectTask = _repo.GetByKey<CountryDetails>("CountryDetails" + cod);
            if (objectTask.Result == null)
            {
                objectTask = _countryDetailsService.GetEntityByKey(cod);

                Thread repoSetThread = new Thread(() =>
                    _repo.SetForKey<CountryDetails>("CountryDetails" + cod, objectTask.Result)
                );
                repoSetThread.Start();
            }

            return await objectTask;
        }
    }
}

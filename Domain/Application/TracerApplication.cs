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
        private readonly IExtServices _extServices;
        private readonly IRepo _repo;
        public TracerApplication(IExtServices extServices,
                                IRepo repo,
                                IServiceScopeFactory serviceScopeFactory)
        {
            _extServices = extServices;
            _repo = repo;
        }

        public async Task<InfoIP> ByIP(string ip)
        {
            InfoIP info = new InfoIP(ip);

            var ip2CountryTask = this.GetFromRepoOrService<Ip2Country>("https://api.ip2country.info/ip?{0}", "Ip2Country", ip);
            var restCountryTask = this.GetFromRepoOrService<List<RestCountry>>("https://restcountries.eu/rest/v2/name/{0}", "RestCountry", ip2CountryTask.Result.countryName);

            info.WithIp2Country(ip2CountryTask.Result)
                .WithRestCountry(restCountryTask.Result.FirstOrDefault());


            return await Task.FromResult(info);
        }

        private async Task<T> GetFromRepoOrService<T>(string urlService, string repoKey, string key)
        {
            var objectTask = _repo.GetByKey<T>(repoKey + key);
            if (objectTask.Result == null)
            {
                var url = string.Format(urlService, key);
                objectTask = _extServices.GetUrlToJson<T>(url);

                Thread repoSetThread = new Thread(() =>
                    _repo.SetForKey<T>(repoKey + key, objectTask.Result)
                );
                repoSetThread.Start();
            }

            return await objectTask;
        }
    }
}

using Domain.Entities;
using Domain.Infrastructure;
using Domain.Infrastructure.ExtServices;
using Domain.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application
{
    public class TracerApplication : ITracerApplication
    {
        private readonly IExtServices _extServices;
        private readonly IRepo _repo;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public TracerApplication(IExtServices extServices,
                                IRepo repo,
                                IServiceScopeFactory serviceScopeFactory)
        {
            _extServices = extServices;
            _repo = repo;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<InfoIP> ByIP(string ip)
        {
            InfoIP info = new InfoIP(ip);

            var ip2CountryTask = this.GetFromRepoOrService<Ip2Country>("https://api.ip2country.info/ip?{0}", "Ip2Country", ip);
            var restCountryTask = this.GetFromRepoOrService<RestCountry>("https://restcountries.eu/rest/v2/name/{0}", "RestCountry", ip2CountryTask.Result.countryName, true);

            info.WithIp2Country(ip2CountryTask.Result)
                .WithRestCountry(restCountryTask.Result);


            return await Task.FromResult(info);
        }

        private async Task<T> GetFromRepoOrService<T>(string urlService, string repoKey, string key, bool isList = false)
        {
            var ip2CountryTask = _repo.GetByKey<T>(repoKey + key);
            if (ip2CountryTask.Result == null)
            {
                var url = string.Format(urlService, key);

                if (isList) {
                    var listTask = _extServices.GetUrlToJsonList<T>(url);
                    ip2CountryTask = Task.FromResult(listTask.Result[0]);
                }
                else { 
                    ip2CountryTask = _extServices.GetUrlToJson<T>(url);
                }

                // Fire off the task, but don't await the result
                await Task.Run(async () => {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var otherScopeRepo = scope.ServiceProvider.GetRequiredService<IRepo>();
                    await otherScopeRepo.SetForKey<T>(repoKey+ key, ip2CountryTask.Result);
                });
            }

            return await ip2CountryTask;
        }

    }
}

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
    public class TracerApplication : ITracerApplication
    {
        //private readonly IRepo _repo;
        //private readonly IServices<IpCountry> _ipCountryService;
        //private readonly IServices<CountryDetails> _countryDetailsService;
        private readonly ICountryApplication _countryApplication;
        //private readonly IServices<Currency> _currencyService;

        public TracerApplication(ICountryApplication countryApplication)
        {
            _countryApplication = countryApplication;
            //_currencyService.GetEntityByKey("");

        }

        public async Task<InfoIP> ByIP(string ip)
        {




            var ipCountryTask = _countryApplication.GetIpCountryFromRepo<IpCountry>(ip);
            if (ipCountryTask.Result == null)
            {
                ipCountryTask = _countryApplication.GetIpCountryFromService(ip);
                _countryApplication.SaveIpCountryToRepo(ip, ipCountryTask.Result);
            }

            string countryCode = ipCountryTask.Result.countryCode;
            var countryDetailsTask = _countryApplication.GetCountryDetailsFromRepo<CountryDetails>(countryCode);
            if (countryDetailsTask.Result == null)
            {
                countryDetailsTask = _countryApplication.GetCountryDetailsFromService(countryCode);
                _countryApplication.SaveCountryDetailsToRepo(countryCode, countryDetailsTask.Result);
            }

            //● Distancia estimada entre Buenos Aires y el país, en km.

            InfoIP info = new InfoIP(ip, ipCountryTask.Result, countryDetailsTask.Result);
            
            info.CalculateCurrentDate();
            return await Task.FromResult(info);
        }



    }
}

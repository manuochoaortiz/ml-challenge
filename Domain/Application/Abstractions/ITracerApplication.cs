using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Abstractions
{
    public interface ITracerApplication
    {
        Task<InfoIP> ByIP(string ip);
    }

    //public interface ICountryApplication
    //{
    //    //Task<InfoIP> ByIP(string ip);
    //    Task<T> GetIpCountryFromRepo<T>(string ip);
    //    Task<IpCountry> GetIpCountryFromService(string ip);
    //    void SaveIpCountryToRepo(string ip, IpCountry entity);

    //    Task<T> GetCountryDetailsFromRepo<T>(string cod);
    //    Task<CountryDetails> GetCountryDetailsFromService(string cod);
    //    void SaveCountryDetailsToRepo(string cod, CountryDetails entity);
    //}
}

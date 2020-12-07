using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ApiService
{
    public interface ITracerApiController
    {
        Task<InfoIP> ByIP(string ip);
    }
}

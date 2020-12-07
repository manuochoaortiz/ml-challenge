using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application
{
    public interface ITracerApplication
    {
        Task<InfoIP> ByIP(string ip);
    }
}

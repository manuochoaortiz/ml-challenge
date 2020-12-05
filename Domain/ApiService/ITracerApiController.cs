using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ApiService
{
    public interface ITracerApiController
    {
        InfoIP ByIP(string ip);
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Application
{
    public interface ITracerApplication
    {
        InfoIP ByIP(string ip);
    }
}

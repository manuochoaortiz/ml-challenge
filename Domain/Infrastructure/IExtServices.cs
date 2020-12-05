using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
    public interface IExtServices<T>
    {
        Task<T> ByIP(string ip);
    }
}

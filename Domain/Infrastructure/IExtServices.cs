using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure
{
    public interface IExtServices<T>
    {
        T ByIP(string ip);
    }
}

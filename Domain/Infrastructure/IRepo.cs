using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure
{
    public interface IRepo<T>
    {
        T ByIP(string ip);
        void ForIP(string ip, T entity);
    }
}

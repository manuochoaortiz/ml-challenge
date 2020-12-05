using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
    public interface IRepo<T>
    {
        Task<T> ByIP(string ip);
        Task ForIP(string ip, T entity);
    }
}

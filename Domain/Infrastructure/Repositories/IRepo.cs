using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Repositories
{
    public interface IRepo
    {
        //Task<T> ByIP(string ip);
        //Task ForIP(string ip, T entity);
        Task<T> GetByKey<T>(string key);
        Task SetForKey<T>(string key, T entity);
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.ExtServices
{
    public interface IServices<T>
    {
        Task<T> GetEntityByKey(string key);
    }
}

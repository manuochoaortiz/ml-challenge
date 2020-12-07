using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.ExtServices
{
    public interface IExtServices
    {
        Task<T> GetUrlToJson<T>(string urlHttp);
    }
}

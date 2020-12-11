using Domain.Entities;
using Domain.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MemoryCacheRepo : IRepo
    {
        private readonly MemoryCache _db = new MemoryCache(new MemoryCacheOptions());

        public Task<T> GetByKey<T>(string key)
        {
            var response = _db.Get<T>(key);
            return Task.FromResult(response);
        }

        public Task SetForKey<T>(string key, T entity)
        {
            //System.Threading.Thread.Sleep(50000);
            _db.Set(key, entity);
            return Task.CompletedTask;
        }
    }
}

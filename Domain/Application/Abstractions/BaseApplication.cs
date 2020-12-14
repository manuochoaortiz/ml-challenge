using Domain.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Abstractions
{
    public abstract class BaseApplication
    {
        private readonly IRepo _genericRepo;

        public BaseApplication(IRepo genericRepo)
        {
            _genericRepo = genericRepo;
        }

        public async Task<T> GetEntityFromRepo<T>(string key)
        {
            var entityTask = _genericRepo.GetByKey<T>(key);
            return await entityTask;
        }

        public async void SaveRepoEntity<T>(string key, T result)
        {
            await Task.Run(() =>
                _genericRepo.SetForKey<T>(key, result)
            );
        }
    }
}

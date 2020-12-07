﻿using Domain.Entities;
using Domain.Infrastructure;
using Domain.Infrastructure.ExtServices;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExtServices
{
    public class ExtServices : IExtServices
    {
        public async Task<T> GetUrlToJson<T>(string urlHttp)
        {
            try
            {
                //string flurlHttp = string.Format(_urlServive, key);
                T entity = await urlHttp.GetJsonAsync<T>();
                return await Task.FromResult(entity);
            }
            catch (Exception ex)
            {
                T entity = default;
                return await Task.FromResult(entity);

            }

        }

        public async Task<T[]> GetUrlToJsonList<T>(string urlHttp)
        {
            try
            {
                //string flurlHttp = string.Format(_urlServive, key);
                T[] entity = await urlHttp.GetJsonAsync<T[]>();
                return await Task.FromResult(entity);
            }
            catch (Exception ex)
            {
                T[] entity = default;
                return await Task.FromResult(entity);

            }

        }
    }
}

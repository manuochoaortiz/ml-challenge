using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IpTracker.Api.Application.Queries;
using IpTracker.Domain.Modules.CounterCountry;
using IpTracker.Domain.Modules.Country;
using IpTracker.Domain.Modules.CountryDetails;
using IpTracker.Domain.Modules.Currency;
using IpTracker.Infrastructure.CounterCountry;
using IpTracker.Infrastructure.Country;
using IpTracker.Infrastructure.CountryDetails;
using IpTracker.Infrastructure.Currency;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace IpTracker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
            });

            //CQRS
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IRequestHandler<GetIpTrackByIpQuery, GetIpTrackByIpViewModel>, GetIpTrackByIpQueryHandler>();
            services.AddTransient<IRequestHandler<GetCounterCountryQuery, GetCounterCountryViewModel>, GetCounterCountryQueryHandler>();

            //Redis
            services.AddSingleton<IConnectionMultiplexer>(x =>
                   ConnectionMultiplexer.Connect(Configuration.GetValue<string>("RedisConnection")));
            //Repositories
            services.AddSingleton<ICountryRepository, CountryRepository>();
            services.AddSingleton<ICountryDetailsRepository, CountryDetailsRepository>();
            services.AddSingleton<ICurrencyRepository, CurrencyRepository>();
            services.AddSingleton<ICounterCountryRepository, CounterCountryRepository>();

            //Services
            services.AddSingleton<ICountryService, CountryService>(serviceProvider => {
                return new CountryService(Configuration.GetValue<string>("ServiceCountry"));
            });
            services.AddScoped<ICountryDetailsService, CountryDetailsService>(serviceProvider => {
                return new CountryDetailsService(Configuration.GetValue<string>("ServiceCountryDetails"));
            });
            services.AddScoped<ICurrencyService, CurrencyService>(serviceProvider => {
                return new CurrencyService(Configuration.GetValue<string>("ServiceCurrency"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

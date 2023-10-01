using Microsoft.Extensions.DependencyInjection;
using ReadAndAnalysis.App.Services.Implementations;
using ReadAndAnalysis.App.Services.Interfaces;
using ReadAndAnalysis.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddDbContext<TxtPrcContext>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<INewsService, NewsService>();

        }
    }
}

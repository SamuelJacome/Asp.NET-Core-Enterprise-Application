using Microsoft.Extensions.DependencyInjection;
using NSE.Catalog.API.Data;
using NSE.Catalog.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Catalog.API.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static void AddDependecyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, IProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}

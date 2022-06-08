using Microsoft.Extensions.DependencyInjection;
using NSE.Catalogo.API.Data.Repository;
using NSE.Catalog.API.Models;
using NSE.Catalog.API.Data;

namespace NSE.Catalog.API.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static void AddDependecyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}

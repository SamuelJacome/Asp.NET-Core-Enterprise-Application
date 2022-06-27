using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSE.Identity.API.Services;
using NSE.Web.MVC.Extensions;
using NSE.Web.MVC.Services;

namespace NSE.Identity.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationServices, AuthenticantionServices>();
            services.AddHttpClient<ICatalogService, CatalogService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
              
        }
    }
}

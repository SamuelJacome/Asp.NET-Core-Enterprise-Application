using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.Identity.API.Services;
using NSE.Web.MVC.Extensions;
using NSE.Web.MVC.Services;
using NSE.Web.MVC.Services.Handlers;
using System;

namespace NSE.Identity.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationServices, AuthenticantionServices>();

            //services.AddHttpClient<ICatalogService, CatalogService>()
            //        .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient(name: "Refit", options => {
                options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
                })
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();
              
        }
    }
}

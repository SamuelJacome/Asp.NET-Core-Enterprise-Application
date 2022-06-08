using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSE.Identity.API.Data;
using NSE.Identity.API.Extensions;
using NSE.WebAPI.Core.Identity;
using System.Text;


namespace NSE.Identity.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(optionsAction: options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
               .AddRoles<IdentityRole>()
               .AddErrorDescriber<IdentityCustomMessages_PtBr>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddJwtConfiguration(Configuration);
           
            return services;
        }

    }
}

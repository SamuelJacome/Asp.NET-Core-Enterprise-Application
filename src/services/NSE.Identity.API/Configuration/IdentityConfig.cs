﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSE.Identity.API.Data;
using NSE.Identity.API.Extensions;

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

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = true;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidIn,
                    ValidIssuer = appSettings.Issuer
                };
            });


            return services;
        }
        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}

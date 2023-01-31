using mediatorCqrs.Application.Contracts.Identity;
using mediatorCqrs.Application.Model.Identity;
using mediatorCqrs.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services , IConfiguration configuration)
        {
            //services.Configure<JWTsetting>(configuration.GetSection("JWTsetting").Value);
            //var r = configuration.GetSection("JWTsetting").Value;
            //configuration.GetSection<JWTsetting>("JWTsetting").Value;


            services.AddScoped<IAthenticationJWTServices, AuthenticationeJWTservice>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            configuration.GetSection("JWTsetting:key").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };

                });

            return services;



            return services;
        }
    }
}

using mediatorCqrs.Application.Model.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            ;
           


            return services;
        }
    }
}

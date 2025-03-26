using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walrus.PrestashopManager.Data.Contracts;
using Walrus.PrestashopManager.UserWebApi.Services.Services.Contracts;
using Walrus.PrestashopManager.UserWebApi.Services.Services.Core;
using Walrus.PrestashopManager.Data.ExtensionMethods;
using Microsoft.Extensions.Configuration;

namespace Walrus.PrestashopManager.UserWebApi.Services.ExtensionMethods
{
    public static class ExtensionMethods
    {


        public static void AddServicesLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayer(configuration);

        }

        public static void RegisterServicesServices(this ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IJwtService, JwtService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.RegisterDataServices();
        }
    }
}

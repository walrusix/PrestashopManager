using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Walrus.PrestashopManager.Data.ExtensionMethods;

namespace Walrus.PrestashopManager.UserPresentation.Services.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static void AddServicesLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayer(configuration);

        }

        public static void RegisterServicesServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterDataServices();
        }

        public static void UseServicesLayer(this IApplicationBuilder app)
        {
            

        }
    }
}

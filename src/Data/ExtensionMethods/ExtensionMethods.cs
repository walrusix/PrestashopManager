using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walrus.PrestashopManager.Data.Contracts;
using Walrus.PrestashopManager.Data.Repositories;
using Walrus.PrestashopManager.Domain.User;

namespace Walrus.PrestashopManager.Data.ExtensionMethods
{
    public static class ExtensionMethods
    {

        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
        }
        public static void RegisterDataServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            serviceCollection.AddScoped<IUserRepository,UserRepository>();
        }


        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseSqlServer(configuration.GetConnectionString("SqlServerMainDb"));
            });
        }
    }
}

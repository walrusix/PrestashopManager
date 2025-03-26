﻿using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Walrus.PrestashopManager.UserWebApi.Infra.Configuration;
using Walrus.PrestashopManager.Utilities;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.Middlewares;
using WebFramework.Swagger;

namespace Walrus.PrestashopManager.UserWebApi.WebApi
{
    public class Startup
    {
        private readonly MainSettings _mainSetting;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _mainSetting = configuration.GetSection(nameof(MainSettings)).Get<MainSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MainSettings>(Configuration.GetSection(nameof(MainSettings)));

            services.InitializeAutoMapper();

            services.AddDbContext(Configuration);

            services.AddCustomIdentity(_mainSetting.IdentitySettings);

            services.AddMinimalMvc();


            services.AddJwtAuthentication(_mainSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddSwagger();

            // Don't create a ContainerBuilder for Autofac here, and don't call builder.Populate()
            // That happens in the AutofacServiceProviderFactory for you.
        }

        // ConfigureContainer is where you can register things directly with Autofac. 
        // This runs after ConfigureServices so the things ere will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Register Services to Autofac ContainerBuilder
            builder.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.IntializeDatabase();

            app.UseCustomExceptionHandler();

            app.UseHsts(env);

            app.UseHttpsRedirection();


            app.UseSwaggerAndUI();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(config =>
            {
                config.MapControllers(); 
            });
        }
    }
}

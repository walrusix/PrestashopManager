using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walrus.PrestashopManager.Data;
using Walrus.PrestashopManager.Utilities;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.Swagger;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Walrus.PrestashopManager.Utilities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Walrus.PrestashopManager.Domain.User;
using Walrus.PrestashopManager.Data.Contracts;
using System.Security.Claims;
using System.Net;
using Walrus.PrestashopManager.Utilities.Utilities;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Builder;
using Walrus.PrestashopManager.UserWebApi.Infra.Configuration;
using WebFramework.Middlewares;
using Microsoft.AspNetCore.Hosting;

namespace Walrus.PrestashopManager.UserWebApi.Infra.ExtenstionMethods
{
    public static class ExtensionMethods
    {
        public static void AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var mainSetting = configuration.GetSection(nameof(MainSettings)).Get<MainSettings>();
            services.InitializeAutoMapper();
            services.AddDbContext(configuration);
            services.AddCustomIdentity(mainSetting.IdentitySettings);
            services.AddMinimalMvc();
            services.AddJwtAuthentication(mainSetting.JwtSettings);
            services.AddCustomApiVersioning();
            services.AddSwagger();
        }

        public static void UseInfraLayer(this IApplicationBuilder app, IWebHostEnvironment env)
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



        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseSqlServer(configuration.GetConnectionString("SqlServerMainDb"));
            });
        }

        private static void AddMinimalMvc(this IServiceCollection services)
        {
            
            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter()); //Apply AuthorizeFilter as global filter to all actions
            }).AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.Converters.Add(new StringEnumConverter());
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }


        private static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptKey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {

                        if (context.Exception != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                        if (!securityStamp.HasValue())
                            context.Fail("This token has no security stamp");

                        var userId = claimsIdentity.GetUserId<int>();
                        var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);


                        var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validatedUser == null)
                            context.Fail("Token security stamp is not valid.");

                        //if (!user.IsActive)
                        //    context.Fail("User is not active.");

                        await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
                    },
                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                        throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                    }
                };
            });
        }

        private static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
        }
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Walrus.PrestashopManager.Data;
using Walrus.PrestashopManager.Data.Contracts;
using Walrus.PrestashopManager.Domain.User;
using Walrus.PrestashopManager.Utilities;
using Walrus.PrestashopManager.Utilities.Exceptions;
using Walrus.PrestashopManager.Utilities.Utilities;

namespace Walrus.PrestashopManager.UserWebApi.Infra.Configuration
{
    public static class ServiceCollectionExtensions
    {
        
    }
}

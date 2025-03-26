using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Walrus.PrestashopManager.Domain.User;
using Walrus.PrestashopManager.UserWebApi.Services.Services.Contracts;
using Walrus.PrestashopManager.Utilities;
using Walrus.PrestashopManager.Utilities.Exceptions;

namespace Walrus.PrestashopManager.UserWebApi.Services.Services.Core
{
    public class TokenService : ITokenService,IScopedDependency
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public TokenService(UserManager<User> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public async Task<JsonResult> GetAsync(string username, string password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var jwt = await _jwtService.GenerateAsync(user);
            return new JsonResult(jwt);
        }
    }
}

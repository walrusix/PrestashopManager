using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Walrus.PrestashopManager.Domain.User;
using Walrus.PrestashopManager.UserWebApi.Services.Services.Contracts;

namespace Walrus.PrestashopManager.UserWebApi.Services.Services.Core
{
    public class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task Create(string email,string phoneNumber,CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                PhoneNumber = phoneNumber
            };
            var result = await _userManager.CreateAsync(user, "Password");
            return ;
        }
    }
}

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
        public UserService()
        {
            
        }
        public async Task Create(string email,string mobile,CancellationToken cancellationToken)
        {
            //var user = new User
            //{
            //    UserName = email,
            //    Email = email,
            //    Mobile = mobile
            //};
            //var result = await userManager.CreateAsync(user, userDto.Password);

            //var result2 = await roleManager.CreateAsync(new Role
            //{
            //    Name = "Admin",
            //    Description = "admin role"
            //});

            //var result3 = await userManager.AddToRoleAsync(user, "Admin");

            ////await userRepository.AddAsync(user, userDto.Password, cancellationToken);
            //return user;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Walrus.PrestashopManager.Domain.Common;

namespace Walrus.PrestashopManager.Domain.User
{
    public class Role : IdentityRole<int>, IEntity
    {



    }
}

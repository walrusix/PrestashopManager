using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Walrus.PrestashopManager.Domain.Common;
using Walrus.PrestashopManager.Domain.Post;

namespace Walrus.PrestashopManager.Domain.User
{
    public class User : IdentityUser<int>, IEntity<int>, IEntityTypeConfiguration<User>
    {

        public DateTimeOffset? LastLoginDate { get; set; }

        //public ICollection<Post.Post> Posts { get; set; }

        public void Configure(EntityTypeBuilder<User> builder)
        {
          
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(100);
        }
    }
}

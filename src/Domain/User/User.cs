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
        //public User()
        //{
        //    IsActive = true;
        //}

        //[Required]
        //[StringLength(100)]
        //public string FullName { get; set; }
        //public int Age { get; set; }
        //public GenderType Gender { get; set; }
        //public bool IsActive { get; set; }

        public string Username { set; get; }
        public string Email { set; get; }
        public string Mobile { set; get; }
        public DateTimeOffset? LastLoginDate { get; set; }

        //public ICollection<Post.Post> Posts { get; set; }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Mobile).IsRequired().HasMaxLength(100);
        }
    }
}

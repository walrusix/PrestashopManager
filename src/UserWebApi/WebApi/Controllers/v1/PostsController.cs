using AutoMapper;
using System;
using Walrus.PrestashopManager.Data.Contracts;
using Walrus.PrestashopManager.Domain.Post;
using Walrus.PrestashopManager.UserWebApi.Infra.Api;
using Walrus.PrestashopManager.UserWebApi.WebApi.Models;

namespace Walrus.PrestashopManager.UserWebApi.WebApi.Controllers.v1
{
    public class PostsController : CrudController<PostDto, PostSelectDto, Post, Guid>
    {
        public PostsController(IRepository<Post> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}

using AutoMapper;
using Walrus.PrestashopManager.Data.Contracts;
using Walrus.PrestashopManager.Domain.Post;
using Walrus.PrestashopManager.UserWebApi.Infra.Api;
using Walrus.PrestashopManager.UserWebApi.WebApi.Models;

namespace Walrus.PrestashopManager.UserWebApi.WebApi.Controllers.v1
{
    public class CategoriesController : CrudController<CategoryDto, Category>
    {
        public CategoriesController(IRepository<Category> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}

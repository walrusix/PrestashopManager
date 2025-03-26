using Walrus.PrestashopManager.Domain.Post;
using Walrus.PrestashopManager.UserWebApi.Infra.Api;

namespace Walrus.PrestashopManager.UserWebApi.WebApi.Models
{
    public class CategoryDto : BaseDto<CategoryDto, Category>
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public string ParentCategoryName { get; set; } //=> mapped from ParentCategory.Name
    }
}

using Services;
using System.Threading.Tasks;
using Walrus.PrestashopManager.Domain.User;

namespace Walrus.PrestashopManager.UserWebApi.Services.Services.Contracts
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}
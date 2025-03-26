using System.Threading.Tasks;
using Walrus.PrestashopManager.Domain.User;

namespace Services
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Walrus.PrestashopManager.UserWebApi.Services.Services.Contracts
{
    public interface ITokenService
    {
        Task<JsonResult> GetAsync(string username, string password, CancellationToken cancellationToken);
    }
}
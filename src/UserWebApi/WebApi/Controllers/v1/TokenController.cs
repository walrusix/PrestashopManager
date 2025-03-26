using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System;
using Walrus.PrestashopManager.UserWebApi.Infra.Api;
using Walrus.PrestashopManager.UserWebApi.WebApi.Models;
using Walrus.PrestashopManager.Utilities.Exceptions;
using Walrus.PrestashopManager.Domain.User;
using Walrus.PrestashopManager.UserWebApi.Services.Services.Contracts;
using Walrus.PrestashopManager.UserWebApi.WebApi.Models.Token;

namespace Walrus.PrestashopManager.UserWebApi.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class TokenController : BaseController
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public virtual async Task<ActionResult> Get([FromForm] TokenGetApiRequestModel requestModel, CancellationToken cancellationToken)
        {
            var result = await _tokenService.GetAsync(requestModel.Username, requestModel.Password, cancellationToken);
            return Ok(result);
        }
    }
}

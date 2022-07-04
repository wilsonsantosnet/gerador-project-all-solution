using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.API;
using IdentityModel.Client;
using Sso.Server.Api.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Common.Domain.Base;
using Common.Domain;
using System.Net.Http;

namespace Sso.Server.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private ILogger _logger;
        private IOptions<ConfigSettingsBase> _configSettingsBase;
        public AuthController(ILoggerFactory logger, IOptions<ConfigSettingsBase> configSettingsBase)
        {
            this._logger = logger.CreateLogger<AuthController>();
            this._configSettingsBase = configSettingsBase;
            this._logger.LogInformation("AccountController init success");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountCredential accountCredencial)
        {
            var result = new HttpResult<TokenResponse>(this._logger);

            var identityEndPoint = this._configSettingsBase.Value.AuthorityEndPoint;
            if (identityEndPoint.IsNull())
                throw new InvalidOperationException("Endpoint invalid");

            //var tokenClient = new TokenClient(identityEndPoint + "/connect/token", accountCredencial.ClientId, accountCredencial.ClientSecret);
            //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(accountCredencial.User, accountCredencial.Password, accountCredencial.Scope);

            var _client = new HttpClient
            {
                BaseAddress = new Uri(identityEndPoint + "/connect/token")
            };
            var tokenResponse = _client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                UserName = accountCredencial.User,
                Password = accountCredencial.Password,
                Scope = accountCredencial.Scope

            }).Result;


            if (tokenResponse.IsError)
                return result.ReturnCustomException(new Exception(tokenResponse.Error), "Sso.Server.Api - Account");

            return result.ReturnCustomResponse(tokenResponse);
        }


    }
}

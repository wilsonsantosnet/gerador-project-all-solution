﻿using Common.Domain.Base;
using Common.Domain.Model;
using IdentityModel.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.API.Extensions
{
    public class RequestTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTokenMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, CurrentUser currentUser, IOptions<ConfigSettingsBase> configSettingsBase)
        {
            var token = context.Request.Headers["Authorization"];
            if (!token.IsNullOrEmpaty())
            {
                var tokenClear = token.ToString().Replace("Bearer", "").Replace(" ","");
                var jwt = new JwtSecurityTokenHandler();
                var canRead = jwt.CanReadToken(tokenClear);
                if (canRead)
                {
                    //var claims = await GetClaimsFromServer(configSettingsBase, tokenClear);
                    var claims = GetClaimsFromUserPrincipal(context);
                    //var claims = GetClaimsFromReadToken(tokenClear, jwt);
                    this.ConfigClaims(currentUser, tokenClear, claims.ConvertToDictionary());
                }
            }
            else
            {
                var claims = GetClaimsFromUserPrincipal(context);
                ConfigClaims(currentUser, string.Empty, claims.ConvertToDictionary());
            }
            await this._next.Invoke(context);
        }

        protected virtual void ConfigClaims(CurrentUser currentUser, string tokenClear, IDictionary<string, object> claimsDictonary)
        {
            currentUser.Init(tokenClear, claimsDictonary);
        }

        private static IEnumerable<Claim> GetClaimsFromReadToken(string tokenClear, JwtSecurityTokenHandler jwt)
        {
            var result = jwt.ReadJwtToken(tokenClear);
            var claims = result.Claims;
            return claims;
        }

        private static IEnumerable<Claim> GetClaimsFromUserPrincipal(HttpContext context)
        {
            var caller = context.User;
            var claims = caller.Claims;
            return claims;
        }

       
    }

    public static class RequestTokenMiddlewareExtension
    {
        public static IApplicationBuilder AddTokenMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestTokenMiddleware>();
        }
    }
}

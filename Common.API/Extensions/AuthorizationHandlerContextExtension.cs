using Common.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.API.Extensions
{
    public static class AuthorizationHandlerContextExtension
    {

        public static Boolean VerifyClaimsCanReadOne(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanReadOne(DefineControllerName(source));

            return false;
        }

        public static Boolean VerifyClaimsCanReadDataItem(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanReadDataItem(DefineControllerName(source));

            return false;
        }

        public static Boolean VerifyClaimsCanReadAll(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanReadAll(DefineControllerName(source));

            return false;
        }

        public static Boolean VerifyClaimsCanEdit(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanEdit(DefineControllerName(source));

            return false;
        }

        public static Boolean VerifyClaimsCanSave(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanSave(DefineControllerName(source));

            return false;
        }


        public static Boolean VerifyClaimsCanWrite(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanWrite(DefineControllerName(source));

            return false;
        }

        public static Boolean VerifyClaimsCanDelete(this AuthorizationHandlerContext source, IEnumerable<Tool> tools)
        {
            if (tools.IsAny())
                return tools.VerifyClaimsCanDelete(DefineControllerName(source));

            return false;
        }

        private static string DefineControllerName(AuthorizationHandlerContext source)
        {

            var controller = ((Microsoft.AspNetCore.Http.DefaultHttpContext)source.Resource).Request.Path.Value.Replace("/{id}", "").Split("/");
            return controller[2];
            //return ((Microsoft.AspNetCore.Routing.RouteEndpoint)source.Resource).RoutePattern.RawText.Replace("/{id}","").Split("/").LastOrDefault();
        }


    }
}

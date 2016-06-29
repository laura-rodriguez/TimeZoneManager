using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace TimeZoneManager.API.Filters
{
    public class WebAPIAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext ctx)
        {
            if (!ctx.RequestContext.Principal.Identity.IsAuthenticated)
                base.HandleUnauthorizedRequest(ctx);
            else
            {
                // Authenticated, but not AUTHORIZED.  Return 403 instead!
                ctx.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
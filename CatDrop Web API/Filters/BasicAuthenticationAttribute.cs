using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using CatDrop.Interfaces;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using CatDrop.Services;


namespace CatDrop.WebApi.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private const string Realm = "Cats";

    //TODO: Injection not working
        [Dependency]
        public IUserService UserService{ get; set; }

        public BasicAuthenticationAttribute()
        {
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
           

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                var userNamePasswordPair = decodedAuthenticationToken.Split(':');

                var userName = userNamePasswordPair[0];
                var password = userNamePasswordPair[1];

                //TODO: Fix filter depencency injection and use infejcted instance
                if ( new UserService(new Logger()).Validate(userName, password))
                //if (new UserService.Validate(userName, password))
                {
                    var identity = new GenericIdentity(userName);
                    IPrincipal principal = new GenericPrincipal(identity, null);
                    Thread.CurrentPrincipal = principal;

                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }

                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}
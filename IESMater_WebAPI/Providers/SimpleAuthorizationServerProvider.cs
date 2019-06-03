using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using IESMater_WebAPI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace IESMater_WebAPI
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        //private readonly string _publicClientId;

     

        //public SimpleAuthorizationServerProvider(string publicClientId)
        //{
        //    if (publicClientId == null)
        //    {
        //        throw new ArgumentNullException("publicClientId");
        //    }

        //    _publicClientId = publicClientId;
        //}

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }

       

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override async Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            context.Validated();
            //if (context.ClientId == _publicClientId)
            //{
            //    Uri expectedRootUri = new Uri(context.Request.Uri, "/");

            //    if (expectedRootUri.AbsoluteUri == context.RedirectUri)
            //    {
            //        context.Validated();
            //    }
            //}

            //return Task.FromResult<object>(null);
        }

      
    }
}
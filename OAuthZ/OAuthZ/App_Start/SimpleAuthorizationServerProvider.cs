using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System;

namespace OAuthZ
{
    internal class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// grant_type = password will invoke this. Should be rejected.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", "jeremy"));
            identity.AddClaim(new Claim("role", "user"));
            context.Validated(identity);
        }
        
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = "jeremy";
            string clientSecret = string.Empty;
            context.TryGetFormCredentials(out clientId, out clientSecret);
            context.OwinContext.Set<string>("as:client_id", clientId);
            context.Validated(clientId);
            return base.ValidateClientAuthentication(context);
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "jeremy"));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            //var oAuthIdentity = new ClaimsIdentity();
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "jeremy"));
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            //var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties { AllowRefresh = true, RedirectUri = "/token", IssuedUtc = DateTime.Now, ExpiresUtc = DateTime.Now.AddMonths(1)});
            //context.Validated(context.Ticket);
                        
            var originalClient = "jeremy";
            var currentClient = context.OwinContext.Get<string>("as:client_id");

            //var newId = new ClaimsIdentity(context.Ticket.Identity);
            var newId = new ClaimsIdentity("refresh_token");
            newId.AddClaim(new Claim("newClaim", "refreshToken"));
            var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
            context.Validated(newTicket);

        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;
using OAuthZ.Controllers;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace OAuthZ
{
    internal class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().ToString("n");
            context.SetToken(refreshTokenId);
        }
        
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {

            var refreshTokenId = Guid.NewGuid().ToString("n");
            context.SetToken(refreshTokenId);

            //string clientid = string.Empty;
            ////context.Ticket.Properties.Dictionary.TryGetValue("as:client_id",  out clientid);

            ////if (string.IsNullOrEmpty(clientid))
            ////{
            ////    return;
            ////}

            //var refreshTokenId = Guid.NewGuid().ToString("n");

            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            //    var token = new RefreshToken()
            //    {
            //        Id = "jeremy", // Helper.GetHash(refreshTokenId),
            //        ClientId = clientid,
            //        Subject = context.Ticket.Identity.Name,
            //        IssuedUtc = DateTime.UtcNow,
            //        ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            //    };

            //    context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            //    context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            //    token.ProtectedTicket = context.SerializeTicket();

            //    //var result = await _repo.AddRefreshToken(token);

            //    //if (result)
            //    //{
            //        context.SetToken(refreshTokenId);
            //    //}
            //}
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            var oAuthIdentity = new ClaimsIdentity();
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "jeremy"));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.SetTicket(ticket);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {

            var oAuthIdentity = new ClaimsIdentity();
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "jeremy"));
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "jeremy"));
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.GivenName, "jeremy woo"));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties { AllowRefresh = true, IsPersistent = false, RedirectUri = "http://localhost", IssuedUtc = DateTime.Now, ExpiresUtc = DateTime.Now.AddMonths(1) });
            ticket.Identity.Label = "tetdemotoken";
            
            context.SetTicket(ticket);
            

            //var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            //if (allowedOrigin != null) 
            //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            //string hashedTokenId = "abc112233"; //Helper.GetHash(context.Token);

            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    var refreshToken = await _repo.FindRefreshToken(hashedTokenId);

            //    if (refreshToken != null)
            //    {
            //        //Get protectedTicket from refreshToken class
            //        context.DeserializeTicket(refreshToken.ProtectedTicket);
            //        var result = await _repo.RemoveRefreshToken(hashedTokenId);
            //    }


            //    var oAuthIdentity = new ClaimsIdentity();
            //    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "jeremy"));
            //    var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());

            //    context.SetTicket(ticket);
            //}
        }
    }
}
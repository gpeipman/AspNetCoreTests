using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCoreTests.IntegrationTests
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IList<Claim> _claims;

        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, 
                               UrlEncoder encoder, ISystemClock clock, IList<Claim> claims) : base(options, logger, encoder, clock) 
        {
            _claims = claims;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = _claims;

            if (claims == null)
            {
                claims = new List<Claim>
                         {
                             new Claim(ClaimTypes.Name, "Test user"),
                             new Claim(ClaimTypes.Role, "Admin"),
                             new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                         };
            }

            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");
            
            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}

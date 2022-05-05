using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreTests.Web.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTests.Web.Extensions
{
    public class AddRolesClaimsTransformation : IClaimsTransformation
    {
        private readonly DemoDbContext _dataContext;

        public AddRolesClaimsTransformation(DemoDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Clone current identity
            var clone = principal.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity;

            // Support AD and local accounts
            var nameId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier ||
                                                              c.Type == ClaimTypes.Name);
            if (nameId == null)
            {
                return principal;
            }

            var roleIds = await _dataContext.UserRoles
                                            .Where(userRole => userRole.UserId == principal.Identity.Name)
                                            .Select(userRole => userRole.RoleId)
                                            .ToListAsync();
            var roles = await _dataContext.Roles
                                          .Where(role => roleIds.Contains(role.Id))
                                          .ToListAsync();

            // Add role claims to cloned identity
            foreach (var role in roles)
            {
                var claim = new Claim(newIdentity.RoleClaimType, role.Name);
                newIdentity.AddClaim(claim);
            }

            return clone;
        }
    }
}

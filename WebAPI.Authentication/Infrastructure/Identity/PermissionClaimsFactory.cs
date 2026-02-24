using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPI.Authentication.Domain.Identity;
using WebAPI.Authentication.Infrastructure.Persistence;

namespace WebAPI.Authentication.Infrastructure.Identity
{
    public class PermissionClaimsFactory    : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        private readonly ApplicationDbContext _db;
        public PermissionClaimsFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> options,
        ApplicationDbContext db)
            : base(userManager, roleManager, options)
        {
            _db = db;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var permissions = await _db.UserRoles
        .Where(ur => ur.UserId == user.Id)
        .Join(
            _db.Roles,
            ur => ur.RoleId,
            r => r.Id,
            (ur, r) => r
        )
        .SelectMany(r => r.RolePermissions)
        .Select(rp => rp.Permission.Code)
        .Distinct()
        .ToListAsync();

            foreach (var permission in permissions)
            {
                identity.AddClaim(new Claim("permission", permission));
            }

            return identity;
        }
    }

}

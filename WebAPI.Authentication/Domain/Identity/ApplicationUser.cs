using Microsoft.AspNetCore.Identity;

namespace WebAPI.Authentication.Domain.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

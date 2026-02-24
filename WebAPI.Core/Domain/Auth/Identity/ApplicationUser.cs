using Microsoft.AspNetCore.Identity;

namespace WebAPI.Core.Domain.Auth.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        //public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public string  Company { get; set; }
    }
}

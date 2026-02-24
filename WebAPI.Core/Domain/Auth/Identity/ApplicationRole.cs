using Microsoft.AspNetCore.Identity;
using WebAPI.Authentication.Domain.Authorization;

namespace WebAPI.Core.Domain.Auth.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
         public ICollection<RolePage> RolePages { get; set; }  
        public ICollection<RoleMenu> RoleMenus { get; set; } 

    }
}

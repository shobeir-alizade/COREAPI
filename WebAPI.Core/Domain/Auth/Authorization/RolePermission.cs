using Microsoft.AspNetCore.Identity;
using WebAPI.Authentication.Domain.Identity;

namespace WebAPI.Core.Domain.Auth.Authorization
{
    public class RolePermission  
    {
        public int RoleId { get; set; }
        public ApplicationRole Role { get; set; } = default!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = default!;
    }
}
